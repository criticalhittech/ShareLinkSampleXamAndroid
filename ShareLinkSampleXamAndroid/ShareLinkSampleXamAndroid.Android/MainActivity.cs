using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace ShareLinkSampleXamAndroid.Droid
{
    [Activity(Label = "ShareLinkSampleXamAndroid", Name = "com.companyname.sharelinksamplexamandroid.MainActivity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            if (Intent.ActionSend.Equals(Intent.Action) && Intent.Type != null && "text/plain".Equals(Intent.Type))
            {
                handleSendUrl();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void handleSendUrl()
        {
            var view = new LinearLayout(this) { Orientation = Orientation.Vertical };

            var url = Intent.GetStringExtra(Intent.ExtraText);
            var urlTextView = new TextView(this) { Gravity = GravityFlags.Center };
            urlTextView.Text = url;
            view.AddView(urlTextView);

            var description = new EditText(this) { Gravity = GravityFlags.Top };
            view.AddView(description);

            new AlertDialog.Builder(this)
                .SetTitle("Save a URL Link")
                .SetMessage("Type a description for your link")
                .SetView(view)
                .SetPositiveButton("Add", (dialog, whichButton) =>
                {
                    var desc = description.Text;

                    FinishAndRemoveTask();
                    FinishAffinity();
                })
                .Show();
        }
    }
}