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
using Anatoli.App.Manager;

namespace LoyaltyAndroid.Fragments
{
    public class ChangePassWithCodeDialog : Android.Support.V4.App.DialogFragment
    {
        string _phoneNumber;
        public ChangePassWithCodeDialog(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.ChangePassWithCodeDialogLayout, container, false);
            var MsgTextView = view.FindViewById<TextView>(Resource.Id.MsgTextView);
            var CodeEditText = view.FindViewById<EditText>(Resource.Id.CodeEditText);
            var PassEditText = view.FindViewById<EditText>(Resource.Id.PassEditText);
            var SendButton = view.FindViewById< Button>(Resource.Id.SendButton);

            MsgTextView.Text = string.Format("لطفا صبور باشید. کد رمز برای تلفن همراه به شماره {0} ارسال خواهد شد", _phoneNumber);
            SendButton.Click += async delegate
            {
                if (string.IsNullOrEmpty(CodeEditText.Text))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                    alert.SetTitle(Resource.String.Error);
                    alert.SetMessage(Resource.String.PleaseEnterPassCode);
                    alert.SetPositiveButton(Resource.String.Ok, delegate { });
                    alert.Show();
                    return;
                }

                UsernameValidator validator = new UsernameValidator();
                var validation = validator.Validate(PassEditText.Text); 
                if (!validation.Result)
                {
                    if (validation.Error == UsernameValidationResult.ErrorCode.Empty)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterPhone);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    if (validation.Error == UsernameValidationResult.ErrorCode.BadSyntax)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.InvalidPassword);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    if (validation.Error == UsernameValidationResult.ErrorCode.Length)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                        alert.SetTitle(Resource.String.Error);
                        alert.SetMessage(Resource.String.PleaseEnterLongerPass);
                        alert.SetPositiveButton(Resource.String.Ok, delegate { });
                        alert.Show();
                    }
                    return;
                }
                ProgressDialog pDialog = new ProgressDialog(Activity);
                pDialog.SetMessage(Resources.GetText(Resource.String.PleaseWait));
                pDialog.Show();
                try
                {
                    var result = await AnatoliUserManager.ResetPasswordByCode(_phoneNumber, PassEditText.Text, CodeEditText.Text);
                    if (result != null)
                    {
                        if (result.IsValid)
                        {
                            Dismiss();
                            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                            alert.SetMessage(Resource.String.PasswordChangedSuccessfully);
                            alert.SetPositiveButton(Resource.String.Ok, delegate { });
                            alert.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            };
            return view;
        }
    }
}