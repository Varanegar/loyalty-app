using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LoyaltyAppLibrary.App;
using LoyaltyAppLibrary.App.Validator;
using Android.Text.Method;
using Anatoli.App.Manager;
using Anatoli.Framework.AnatoliBase;

namespace LoyaltyAndroid.Fragments
{
    public class RegisterDialogFragment : Android.Support.V4.App.DialogFragment
    {
        EditText mUserNameEditText;
        EditText mEmailEditText;
        EditText mPasswordEditText;
        Button mRegisterButton;
        CheckBox mShowPasswordCheckBox;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.RegisterDialogLayout, container, false);
            mUserNameEditText = view.FindViewById<EditText>(Resource.Id.UserNameEditText);
            mEmailEditText = view.FindViewById<EditText>(Resource.Id.EmailEditText);
            mPasswordEditText = view.FindViewById<EditText>(Resource.Id.PasswordEditText);
            mShowPasswordCheckBox = view.FindViewById<CheckBox>(Resource.Id.ShowPasswordCheckBox);
            mShowPasswordCheckBox.Click += delegate
            {
                if (mShowPasswordCheckBox.Checked)
                    mPasswordEditText.TransformationMethod = HideReturnsTransformationMethod.Instance;
                else
                    mPasswordEditText.TransformationMethod = PasswordTransformationMethod.Instance;
            };
            mRegisterButton = view.FindViewById<Button>(Resource.Id.RegisterButton);
            mRegisterButton.Click += async delegate
            {
                var phoneValidator = new PhoneValidator();
                var result = phoneValidator.Validate(mUserNameEditText.Text);
                if (!result.Result)
                {
                    if (result.Error == PhoneValidationResult.ErrorCode.empty)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterUserName);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                    else
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.InvalidUserName);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                }

                var emailValidator = new EmailValidator();
                var resulte = emailValidator.Validate(mEmailEditText.Text);
                if (!resulte.Result)
                {
                    if (resulte.Error == EmailValidationResult.ErrorCode.Empty)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterEmail);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                    else if (resulte.Error == EmailValidationResult.ErrorCode.BadSyntax)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.InCorrectEmail);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                }

                UsernameValidator userNameValidator = new UsernameValidator();
                var resultp = userNameValidator.Validate(mPasswordEditText.Text);
                if (!resultp.Result)
                {
                    if (resultp.Error == UsernameValidationResult.ErrorCode.Empty)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterPassword);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                    else
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.InvalidPassword);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                        return;
                    }
                }
                if (!AnatoliClient.GetInstance().WebClient.IsOnline())
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(Resource.String.PleaseConnectToInternet);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    return;
                }
                ProgressDialog progressDialog = new ProgressDialog(Activity);
                progressDialog.SetMessage(Resources.GetString(Resource.String.PleaseWait));
                progressDialog.Show();
                try
                {
                    var regResult = await AnatoliUserManager.RegisterAsync(mPasswordEditText.Text, mPasswordEditText.Text, mUserNameEditText.Text, mEmailEditText.Text);
                    progressDialog.Dismiss();
                    if (regResult != null)
                    {
                        if (regResult.IsValid)
                        {
                            Dismiss();
                            ConfirmUserDialogFragment dialog = new ConfirmUserDialogFragment();
                            dialog.Show(FragmentManager, "ConfirmUserDialogFragment");
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.RegisterFailed);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                        }
                    }
                }
                catch (NoInternetAccessException)
                {
                    progressDialog.Dismiss();
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetMessage(Resource.String.PleaseConnectToInternet);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                }
                catch (Exception ex)
                {
                    progressDialog.Dismiss();
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetMessage(Resource.String.RegisterFailed);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    progressDialog.Dismiss();
                }
            };
            return view;
        }
    }
}