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
using LoyaltyAppLibrary.App.Validator;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Manager;

namespace LoyaltyAndroid.Fragments
{
    public class ForgotPasswordDialog : Android.Support.V4.App.DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.ForgotPasswordDialogLayout, container, false);
            var SendButton = view.FindViewById<Button>(Resource.Id.SendButton);
            var PhoneNumberEditText = view.FindViewById<EditText>(Resource.Id.PhoneNumberEditText);
            SendButton.Click += async delegate
            {
                var phoneNumberValidator = new PhoneValidator();
                var validation = phoneNumberValidator.Validate(PhoneNumberEditText.Text);
                if (!validation.Result)
                {
                    if (validation.Error == PhoneValidationResult.ErrorCode.BadSyntax)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.InvalidPhoneNumber);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    if (validation.Error == PhoneValidationResult.ErrorCode.empty)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterPhone);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    return;
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
                progressDialog.SetMessage(Resources.GetText(Resource.String.PleaseWait));
                progressDialog.Show();
                try
                {
                    var result = await AnatoliUserManager.SendPassCode(PhoneNumberEditText.Text);
                    progressDialog.Dismiss();
                    if (result != null)
                    {
                        if (result.IsValid)
                        {
                            Dismiss();
                            ChangePassWithCodeDialog dialog = new ChangePassWithCodeDialog(PhoneNumberEditText.Text);
                            dialog.Show(FragmentManager, "ChangePassWithCodeDialog");
                        }
                    }
                }
                catch (AnatoliWebClientException ex)
                {
                    progressDialog.Dismiss();
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(ex.MetaInfo.ModelStateString);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    return;
                }
                catch (ServerUnreachableException)
                {
                    progressDialog.Dismiss();
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(Resource.String.ConnectionFailed);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    progressDialog.Dismiss();
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(Resource.String.UnknownError);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    return;
                }
            };
            return view;
        }
    }
}