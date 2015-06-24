using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTicketsWpfApplication.Models
{
    public enum DialogType { About, Login, Close, Notification };

    public class DialogMessage
    {
        public DialogType DlgType { get; set; }

        public ActionType ActType { get; set; }

        public string DialogTemplateKey { get; set; }
    }
}
