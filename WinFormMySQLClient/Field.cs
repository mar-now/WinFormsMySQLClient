using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormMySQLClient
{
    public class Field : FlowLayoutPanel
    {
        public Label Label { get; private set; }
        public TextBox TextBox { get; private set; }

        public Field(string label_text)
            : base()
        {
            this.Size = new Size(1, 1);
            AutoSize = true;

            Label = new Label();
            Label.Text = label_text;
            Label.AutoSize = true;
            Label.Anchor = AnchorStyles.Left;
            Label.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Add(Label);

            TextBox = new TextBox();

            Controls.Add(TextBox);
        }
    }
}
