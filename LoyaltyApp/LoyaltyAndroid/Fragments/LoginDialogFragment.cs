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
using Anatoli.App.Manager;
using Anatoli.Framework.AnatoliBase;
using LoyaltyAppLibrary.App.Validator;

namespace LoyaltyAndroid.Fragments
{
    public class LoginDialogFragment : Android.Support.V4.App.DialogFragment
    {
        EditText mUserNameEditText;
        EditText mPasswordEditText;
        Button mLoginButton;
        TextView mForgotPasswordTextView;
        Button mRegisterButton;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.LoginDialogLayout, container, false);
            mUserNameEditText = view.FindViewById<EditText>(Resource.Id.UserNameEditText);
            mPasswordEditText = view.FindViewById<EditText>(Resource.Id.PasswordEditText);
            mLoginButton = view.FindViewById<Button>(Resource.Id.LoginButton);
            mForgotPasswordTextView = view.FindViewById<TextView>(Resource.Id.ForgotPasswordTextView);
            mRegisterButton = view.FindViewById<Button>(Resource.Id.RegisterButton);
            Dialog.SetCanceledOnTouchOutside(false);
            mLoginButton.Click += async delegate
            {
                try
                {
                    UsernameValidator userNameValidator = new UsernameValidator();
                    var result = userNameValidator.Validate(mUserNameEditText.Text);
                    if (!result.Result)
                    {
                        if (result.Error == UsernameValidationResult.ErrorCode.Empty)
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.PleaseEnterUserName);
                            alert.SetTitle(Resource.String.Error);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                            return;
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.InvalidUserName);
                            alert.SetTitle(Resource.String.Error);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                            return;
                        }
                    }
                    var result2 = userNameValidator.Validate(mPasswordEditText.Text);
                    if (!result2.Result)
                    {
                        if (result.Error == UsernameValidationResult.ErrorCode.Empty)
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.PleaseEnterPassword);
                            alert.SetTitle(Resource.String.Error);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                            return;
                        }
                        else
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.InvalidPassword);
                            alert.SetTitle(Resource.String.Error);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                            return;
                        }
                    }
                    ProgressDialog progressDialog = new ProgressDialog(Activity);
                    progressDialog.SetMessage(Resources.GetString(Resource.String.PleaseWait));
                    progressDialog.Show();
                    try
                    {
                        var username = mUserNameEditText.Text;
                        var password = mPasswordEditText.Text;
                        var user = await AnatoliUserManager.LoginAsync(username, password);
                        if (user != null)
                        {
                            if (user.IsValid)
                            {
                                Console.WriteLine("logged in");
                            }
                        }
                    }
                    catch (TokenException)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetMessage(Resource.String.UserNameOrPasswordIncorrect);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    catch (UnConfirmedUserException)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetMessage(Resource.String.YouAreNotConfrimed);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        progressDialog.Dismiss();
                    }
                }
                catch (Exception)
                {

                }

            };
            mRegisterButton.Click += delegate
            {
                Dismiss();
                RegisterDialogFragment registerDialog = new RegisterDialogFragment();
                registerDialog.Show(FragmentManager, "RegisterDialogFragment");
            };
            mForgotPasswordTextView.Click += delegate
            {
                Dismiss();
                ForgotPasswordDialog forgotPasswordDialog = new ForgotPasswordDialog();
                forgotPasswordDialog.Show(FragmentManager, "ForgotPasswordDialog");
            };
            return view;
        }
    }
}