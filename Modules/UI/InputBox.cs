using System;
using System.Windows.Forms;

namespace NovelArm
{
    internal static class InputBox
    {

        internal static DialogResult ShowDialog(ref string input, string title, string description)
        {
            System.Drawing.Size size = new System.Drawing.Size(500, 150);
            Form inputBox = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = size,
                Text = title,
                TopMost = true
            };

            TextBox textBox = new TextBox 
            {
                Size = new System.Drawing.Size(size.Width - 20, 23),
                Location = new System.Drawing.Point(10, size.Height - 33),
                Text = input,
                Multiline = false
            };
            /*
            if (multiLine)
            {
                textBox.Height += 46;
                inputBox.Height += 46;
            }*/
            inputBox.Controls.Add(textBox);

            Button okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new System.Drawing.Size(75, 25),
                Text = "확인",
                Location = new System.Drawing.Point(size.Width - 85, 10)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Name = "cancelButton",
                Size = new System.Drawing.Size(75, 25),
                Text = "취소",
                Location = new System.Drawing.Point(size.Width - 85, 40)
            };
            inputBox.Controls.Add(cancelButton);

            Label descriptionLabel = new Label
            {
                Name = "descriptionLabel",
                Text = description,
                AutoSize = false,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(size.Width - 105, textBox.Top - 25)
            };
            inputBox.Controls.Add(descriptionLabel);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            inputBox.Dispose();

            return result;
        }
    }
}
