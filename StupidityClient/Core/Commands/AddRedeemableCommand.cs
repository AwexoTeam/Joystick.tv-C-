using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidityClient.Core.Commands
{
    public class AddRedeemableCommand : CommandBase
    {
        public override string Name => "addredeem";

        public override bool isAdmin => true;

        public override void Execute(ChatMessage msg, string[] args)
        {
            if(args.Length <= 1)
            {
                BotManager.SendMessage("Need atleast two args see help for further info.");
                return;
            }

            string name = args[0];
            string priceStr = args[1];

            int price = 0;
            if(!int.TryParse(priceStr, out price))
            {
                BotManager.SendMessage($"{priceStr} is not a valid number!");
                return;
            }

            if(args.Length < 3)
            {
                Redeemables.redeemables.Add(name, price);
                Redeemables.Save();
                BotManager.SendMessage($"{name} for {price} has been added!");
                return;
            }

            Redeemables.tempRedeems.Add(name, price);
            BotManager.SendMessage($"{name} for {price} has been added as temporily. This lasts for this stream!");
        }

        public override string GetHelp(ChatMessage msg, string[] args)
        {
            string help = "";

            return help;
        }
    }
}
