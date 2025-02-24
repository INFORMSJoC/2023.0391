using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IpInputExt.Ctrls
{
    public class NumberTextBoxExt : TextBox
    {
        public NumberTextBoxExt()
            : base()
        {
            this.ShortcutsEnabled = false;
            this.MaxLength = 3;
            this.BorderStyle = BorderStyle.None;
            this.TextAlign = HorizontalAlignment.Center;
            this.Size = new System.Drawing.Size(30, 14);
        }

        /// <summary>
        /// 键盘输入控制
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override bool PreProcessMessage(ref Message m)
        {
            //Console.WriteLine(m.WParam.ToInt32().ToString());
            if (m.WParam.ToInt32() >= (int)'0' && m.WParam.ToInt32() <= (int)'9')
            {//数字键0-9
                return base.PreProcessMessage(ref m);
            }
            else if (m.WParam.ToInt32() >= 96 && m.WParam.ToInt32() <= 105)
            {//数字键盘0-9
                return base.PreProcessMessage(ref m);
            }
            else if (m.WParam.ToInt32() >= 37 && m.WParam.ToInt32() <= 40)
            {//方向控制键：上下左右
                return base.PreProcessMessage(ref m);
            }
            else if (m.WParam.ToInt32() == 8)
            {//删除键
                return base.PreProcessMessage(ref m);
            }
            else if (m.WParam.ToInt32() == 9)
            {
                return base.ProcessKeyMessage(ref m);
            }
            else if (m.WParam.ToInt32() == 110 || m.WParam.ToInt32() == 190)
            {//如果输入“.”则切换到下一个
                m.WParam = new IntPtr(9);
                return base.ProcessKeyMessage(ref m);
            }
            else if (m.WParam.ToInt32() == 46)
            {//Del键
                return base.PreProcessMessage(ref m);
            }
            else
            {
                return true;
            }
        }
    }
}
