

using fNbt;
using System.Drawing;
using System.Windows.Forms;

namespace MCGT_SignTranslator.GTaylor.Data
{
    class SignNode : TreeNode
    {
        public NbtCompound SignData;

        public SignNode(NbtCompound data, string text)
        {
            SignData = data;
            this.Text = text;
            this.BackColor= Color.Green;
        }
    }
}
