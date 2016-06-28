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
            return view;
        }
    }
}