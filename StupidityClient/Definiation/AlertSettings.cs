using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AlertSettings
{
    public static AlertSettings settings { get { return DataManager.alertSettings; } }

    public AlertSettings()
    {
        redeemText = "{user} just redeemed '{info}'";
        followText = "{user} just followed!";
        subscribeText = "{user} just subscribed!";
        tipText = "{user} just tipped {info}";

        fontSize = 28;
        fontFamily = "Microsoft Sans Serif";
        fontStyle = FontStyle.Bold;
        fontColor = Color.FromArgb(255, 255, 255, 255);
    }

    public int alertDelay { get; set; }
    public Color fontColor { get; set; }
    public int fontSize { get; set; }
    public string fontFamily { get; set; }
    public FontStyle fontStyle { get; set; }

    public string redeemPicture { get; set; }
    public string followPicture { get; set; }
    public string tipPicture { get; set; }
    public string subscribePicture { get; set; }

    public string redeemSound { get; set; }
    public string followSound { get; set; }
    public string tipSound { get; set; }
    public string subscribeSound { get; set; }

    public string redeemText { get; set; }
    public string followText { get; set; }
    public string tipText { get; set; }
    public string subscribeText { get; set; }

    public string GetAlertPicture(AlertType type)
    {
        switch (type)
        {
            case AlertType.Redeem:
                return redeemPicture;
            case AlertType.Followed:
                return followPicture;
            case AlertType.Tipped:
                return tipPicture;
            case AlertType.Subscribed:
                return subscribePicture;
            default:
                return "";
        }
    }

    public string GetAlertSound(AlertType type)
    {
        switch (type)
        {
            case AlertType.Redeem:
                return redeemSound;
            case AlertType.Followed:
                return followSound;
            case AlertType.Tipped:
                return tipSound;
            case AlertType.Subscribed:
                return subscribeSound;
            default:
                return "";
        }
    }

    public string GetAlertText(AlertType type)
    {
        switch (type)
        {
            case AlertType.Redeem:
                return redeemText;
            case AlertType.Followed:
                return followText;
            case AlertType.Tipped:
                return tipText;
            case AlertType.Subscribed:
                return subscribeText;
            default:
                return "{user} just {type} {info}";
        }
    }
}