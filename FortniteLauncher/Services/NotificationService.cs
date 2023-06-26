using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace FortniteLauncher.Services
{
    public class NotificationService
    {
        public static bool bToastFailedInit = true;
        public static void SendSimpleToast(string title, string message,string pid,  double ExpirationTime)
        {
            var toast1 = new AppNotificationBuilder()
                .AddText(title)
                .AddText(message)
                .BuildNotification();

            var xmlPayload = new string($@"
        <toast>    
            <visual>    
                <binding template=""ToastGeneric"">    
                    <text>{title}</text>
                    <text>{message}</text>    
                    <text>{pid}</text>    
                </binding>
            </visual>  
        </toast>");

            var toast = new AppNotification(xmlPayload);
            toast.Expiration = DateTimeOffset.Now.AddSeconds(ExpirationTime);

            if (bToastFailedInit)
            {
                return;
            }
            else
            {
                try
                {
                    AppNotificationManager.Default.Show(toast);
                }
                catch (Exception)
                {
                    DialogService.ShowSimpleDialog("Failed to show a notification with title " + title + " and message " + message + " that was set to expire in " + ExpirationTime.ToString(), "Error");
                    throw;
                }
            }
        }

        public static void InitToast()
        {
            try
            {
                AppNotificationManager.Default.Register();
                bToastFailedInit = false;
            }
            catch (Exception ex)
            {
                bToastFailedInit = true;
            }
        }
    }
}
