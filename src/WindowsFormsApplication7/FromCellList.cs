using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApplication7
{

    /// <summary>
    ///二维List
    /// </summary>
    public class FromCellList
    {
        public List<List<object>> list = new List<List<object>>();
        public FromCellList()
        {

        }
        public FromCellList(FromCellList fc)
        {
            list.Clear();
            for (int i = 0; i < fc.list.Count; i++)
            {
                list.Add(fc.list[i]);
            }
        }
        public FromCellList(List<List<object>> fc)
        {
            list.Clear();
            for (int i = 0; i < fc.Count; i++)
            {
                list.Add(fc[i]);
            }
        }
        public FromCellList(object[][] fc)
        {
            list.Clear();
            for (int i = 0; i < fc.Length; i++)
            {
                list.Add(fc[i].ToList());
            }
        }

        /// <summary>
        /// 在行尾添加一行
        /// </summary>
        /// <param name="line"></param>
        public void AppendRow(List<object> line)
        {
            if (RowCount == 0)
            {
                list.Add(line);
                return;
            }

            if (line.Count < 0)
            {
                throw new Exception("添加的行的数据个数不对 ColumnCount");
            }
            else
            {
                list.Add(line);
            }
        }
        /// <summary>
        /// 多少行
        /// </summary>
        public int RowCount
        {
            get
            {
                return list.Count;
            }
        }
        /// <summary>
        /// 多少列
        /// </summary>
        public int ColumnCount
        {
            get
            {
                if (list.Count == 0)
                    return 0;
                return list[0].Count;
            }
        }

        /// <summary>
        /// 检查列的下标是否越界
        /// </summary>
        /// <param name="columnIndex"></param>
        void SafeCheakColumnIndex(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnCount)
            {
                //throw new Exception("列的下标越界");
                Console.WriteLine("行下标越界index");

            }
        }

        /// <summary>
        /// 检查行的下标是否越界
        /// </summary>
        /// <param name="columnIndex"></param>
        void SafeCheakRowIndex(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowCount)
            {
                // throw new Exception("行下标越界index");
                Console.WriteLine("行下标越界index");
            }
        }

        /// <summary>
        /// 删除columnIndex列中_后重复数据所在的行,重复的只保留一份(第一次出现的)
        /// 例子:列 ={1,2,2,3,4,5,5} ,结果={1,2,3,4,5} 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string DelRepeatColumn(int columnIndex)
        {
            var columnList = GetColumn(columnIndex);
            var delList = GetRepeatIndex(columnList);
            var debugOutputStr = DelRowsByListIndex(delList, columnIndex);
            return debugOutputStr;
        }

        /// <summary>
        /// columnIndex列中的值在BaoList重复,则删除所在的行
        /// 例子:列={1,2,3,4,5},BaoList={1,3},结果列={2,4,5}
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="BaoList"></param>
        /// <returns></returns>
        public string DelRowsByListRepeat(int columnIndex, List<object> BaoList)
        {
            SafeCheakColumnIndex(columnIndex);
            var wantDelList = GetColumn(columnIndex);
            var debugOutputStr = "";
            for (int i = wantDelList.Count - 1; i > -1; i--)
            {
                for (int j = 0; j < BaoList.Count; j++)
                {
                    var a = wantDelList[i].ToString();
                    var b = BaoList[j].ToString();
                    if (a == b)
                    {
                        DeleteRow(i);
                        debugOutputStr += wantDelList[i].ToString() + "\r\n";
                    }
                }
            }
            return debugOutputStr;
        }

        /// <summary>
        /// 删除后重复的数据,重复的值只保留一份
        /// </summary>
        /// <param name="list_int"></param>
        /// <returns></returns>
        static List<int> DelRepeat(List<int> list_int)
        {
            for (int i = 0; i < list_int.Count; i++)
            {
                for (int j = list_int.Count - 1; j > i; j--) //j>i 的意思是:>i前面的已经比较过了
                {

                    var A = list_int[i];
                    var B = list_int[j];
                    if (A == B)
                    {
                        list_int.RemoveAt(j);
                    }
                }
            }
            return list_int;
        }

        /// <summary>
        /// 得到后重复的数据的下标,重复的值只保留一份,
        /// </summary>
        /// <param name="list_int"></param>
        /// <returns></returns>
        static List<int> GetRepeatIndex(List<object> list_int)
        {
            var delList = new List<int>();

            for (int i = 0; i < list_int.Count - 1; i++)
            {
                for (int j = list_int.Count - 1; j > i; j--)
                {
                    var A = list_int[i].ToString();
                    var B = list_int[j].ToString();
                    if (A == B)
                    {
                        delList.Add(j);
                    }
                }
            }
            // delList.ForEach(item => Console.WriteLine("Count=" + list.Count + ",delIndex=" + item));
            return delList;
        }

        /// <summary>
        /// 获取下标为columnIndex列的内容
        /// </summary>
        /// <param name="columnIndex">列的下标</param>
        /// <returns></returns>
        public List<object> GetColumn(int columnIndex)
        {
            SafeCheakColumnIndex(columnIndex);
            var tempList = new List<object>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    //有点行是空行 用一个Na代替
                    if (list[i].Count < columnIndex)
                    {
                        tempList.Add(null);
                    }

                    if (j == columnIndex)
                    {
                        tempList.Add(list[i][j]);
                    }
                }
            }
            if (list.Count != tempList.Count)
            {
                // Console.WriteLine("应该得到:" + list.Count + "行,实际得到:" + tempList.Count);
                throw new Exception("行数不对应");
            }
            return tempList;
        }

        /// <summary>
        /// 获取下标为 rowIndex 行的内容
        /// </summary>
        /// <param name="rowIndex">列的下标</param>
        /// <returns></returns>
        public List<object> GetRow(int rowIndex)
        {
            SafeCheakRowIndex(rowIndex);
            return list[rowIndex];
        }


        /// <summary>
        /// 获取最后一行的内容
        /// </summary>
        /// <param name="rowIndex">列的下标</param>
        /// <returns></returns>
        public List<object> GetEndRow()
        {
            var rowIndex = RowCount - 1;
            SafeCheakRowIndex(rowIndex);
            return list[rowIndex];
        }

        /// <summary>
        ///行排序根据columnIndex列的值 从小到大
        /// </summary>
        /// <param name="columnIndex"></param>
        public void Sort(int columnIndex)
        {
            //第一行 标题 不参与 排序
            var firstRow = list[0];
            list.RemoveAt(0);

            list.Sort((left, right) =>
            {
                var l = (List<object>)left;
                var r = (List<object>)right;

                if (l == null || r == null)
                    return -1;

                //有的行 是空行
                if (l.Count < columnIndex || r.Count < columnIndex)
                    return -1;

                //首先比较第一个字段.
                var firstL = -999;
                var firstR = -999;

                int.TryParse(l[columnIndex] as string, out firstL);
                int.TryParse(r[columnIndex] as string, out firstR);

                if (firstL > firstR)
                {
                    return 1;
                }
                else if (firstL == firstR)//相等则比较第2个字段.
                {
                    return 0;
                }
                else
                {
                    return -1;
                }

            });

            //第一行 标题 不参与 排序
            list.Insert(0, firstRow);
        }

        /// <summary>
        ///行排序根据columnIndex的列值 从大到小
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public void SortDescending(int columnIndex)
        {
            //第一行 标题 不参与 排序
            var firstRow = list[0];
            list.RemoveAt(0);

            list.Sort((left, right) =>
            {
                var l = (List<object>)left;
                var r = (List<object>)right;

                if (l == null || r == null)
                    return -1;

                //有的行 是空行
                if (l.Count < columnIndex || r.Count < columnIndex)
                    return -1;

                //首先比较第一个字段.
                var firstL = -999;
                var firstR = -999;

                int.TryParse(l[columnIndex] as string, out firstL);
                int.TryParse(r[columnIndex] as string, out firstR);

                if (firstL > firstR)
                {
                    return -1;
                }
                else if (firstL == firstR)//相等则比较第2个字段.
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            });

            //第一行 标题 不参与 排序
            list.Insert(0, firstRow);
        }

        /// <summary>
        /// 方便输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                var lines = list[i];
                str = "";
                str += "(" + i + "行:" + lines.Count + "列" + ")";
                for (int j = 0; j < lines.Count; j++)
                {
                    if (j > 0)
                    {
                        str += "\t";
                    }

                    str += lines[j].ToString();
                }
                result += str + "\r\n";
            }
            return result;
        }

        /// <summary>
        /// 删除下标为index的列
        /// </summary>
        /// <param name="columnIndex"></param>
        public void DeleteColumn(int columnIndex)
        {
            SafeCheakColumnIndex(columnIndex);
            for (int i = 0; i < list.Count; i++)
            {
                List<object> ziList = list[i];
                for (int j = ziList.Count - 1; j > -1; j--)
                {
                    if (j == columnIndex)
                    {
                        string item = ziList[j].ToString();
                        // Console.WriteLine("Delete:"+item+"\t"+"j="+j);
                        ziList.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// 删除下标为index的行
        /// </summary>
        public void DeleteRow(int rowIndex)
        {
            SafeCheakRowIndex(rowIndex);
            if (rowIndex == 0) return;//标题行不参与操作
            list.RemoveAt(rowIndex);
        }

        /// <summary>
        /// 修改下标为index行
        /// </summary>
        public void UpdateRow(int rowIndex, List<object> rowList)
        {
            SafeCheakRowIndex(rowIndex);
            if (rowList.Count == ColumnCount)
            {
                list[rowIndex] = rowList;
            }

        }

        /// <summary>
        /// 修改下标为index列
        /// </summary>
        /// <param name="columnIndex"></param>
        public void UpdateColumn(int columnIndex, List<object> columnList)
        {
            SafeCheakColumnIndex(columnIndex);
            if (columnList.Count != RowCount)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == columnIndex)
                    {
                        list[i][j] = columnList[i];
                    }
                }
            }
        }

        /// <summary>
        /// 删除delList保存中的下标 所在行
        /// </summary>
        /// <param name="delList"></param>
        public string DelRowsByListIndex(List<int> delList, int ColumnIndex)
        {

            //去除重复的下标
            delList = DelRepeat(delList);

            //Console.WriteLine(delList.ToString());
            //Console.WriteLine("去重复");
            //delList.ForEach(item => Console.WriteLine("Count=" + list.Count + ",delIndex=" + item+"值:"));

            //删除前 先排序 从大到小
            delList.Sort((x, y) => -x.CompareTo(y));

            // Console.WriteLine("排序");
            //delList.ForEach(item => Console.WriteLine("Count=" + list.Count + ",delIndex=" + item));
            // Console.WriteLine("开始del");

            var outPutstr = "";
            for (int i = 0; i < delList.Count; i++)
            {
                if (delList[i] != 0) //标题行 不参与操作
                {
                    // Console.WriteLine("Count=" + list.Count + ",delIndex=" + delList[i]);
                    outPutstr += "行下标:" + delList[i] + "\t" + list[delList[i]][ColumnIndex].ToString() + "\r\n";
                    DeleteRow(delList[i]);
                }

            }
            return outPutstr += "删除数量:" + delList.Count + "个" + "\r\n";
        }

        /// <summary>
        /// columnIndex列中,单元格值的长度等于len,则删除所在的行
        /// </summary>
        /// <param name="len"></param>
        /// <param name="columnIndex"></param>
        public string DelRowEqualsLen(int len, int columnIndex)
        {
            SafeCheakColumnIndex(columnIndex);
            var outPutstr = "";
            var rowList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == columnIndex && i != 0) //i!=0 表示标题 不参与
                    {
                        string item = list[i][j].ToString();
                        if (item.Length == len)
                        {
                            //Console.WriteLine(i);
                            outPutstr += item + "\r\n";
                            rowList.Add(i);
                        }
                    }
                }
            }

            //List遍历中删除指定元素
            for (int i = rowList.Count - 1; i >= 0; i--)
            {
                DeleteRow(rowList[i]);
            }
            return outPutstr += "删除数量:" + rowList.Count + "个" + "\r\n";
        }

        /// <summary>
        ///columnIndex列中,单元格值的长度小于len,则删除所在的行
        /// </summary>
        /// <param name="len"></param>
        /// <param name="columnIndex"></param>
        public string DelRowLessLen(int len, int columnIndex)
        {
            SafeCheakColumnIndex(columnIndex);
            var outPutstr = "";
            var rowList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == columnIndex && i != 0) //i!=0 表示标题 不参与
                    {
                        string item = list[i][j].ToString();
                        if (item.Length < len)
                        {
                            outPutstr += item + "\r\n";
                            rowList.Add(i);
                        }
                    }
                }
            }
            //List遍历中删除指定元素
            for (int i = rowList.Count - 1; i >= 0; i--)
            {
                DeleteRow(rowList[i]);
            }
            return outPutstr += "删除数量:" + rowList.Count + "个" + "\r\n";
        }

        /// <summary>
        ///columnIndex列中,单元格值的长度大于len,则删除所在的行
        /// </summary>
        /// <param name="len"></param>
        /// <param name="columnIndex"></param>
        public string DelRowGreaterLen(int len, int columnIndex)
        {

            SafeCheakColumnIndex(columnIndex);
            var outPutstr = "";

            var rowList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == columnIndex && i != 0) //i!=0 表示标题 不参与
                    {
                        string item = list[i][j].ToString();
                        if (item.Length > len)
                        {
                            outPutstr += item + "\r\n";
                            rowList.Add(i);
                        }
                    }
                }
            }
            //List遍历中删除指定元素
            for (int i = rowList.Count - 1; i >= 0; i--)
            {
                DeleteRow(rowList[i]);
            }
            return outPutstr += "删除数量:" + rowList.Count + "个" + "\r\n";
        }

        /// <summary>
        /// DataTable类型转FromCellList
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static FromCellList DataTableToFromCellList(DataTable dt)
        {
            FromCellList fcl = new FromCellList();
            var objList = new List<object>();
            foreach (DataColumn dc in dt.Columns)
            {
                objList.Add(dc.ColumnName);
            }
            fcl.AppendRow(objList);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dataRow = dt.Rows[i];
                fcl.AppendRow(dataRow.ItemArray.ToList());
                //for (int j = 0; j < dt.Columns.Count; j++)
                //{
                //    string strName = dt.Rows[i][j].ToString();
                //    Console.WriteLine(strName);
                //}
            }
            return fcl;
        }
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < list.Count; i++)
            {
                var lines = list[i];
                var row = new List<string>();
                for (int j = 0; j < lines.Count; j++)
                {
                    string contant = "";
                    if (lines[j].GetType() == typeof(string))
                    {
                        contant = Convert.ToString(lines[j]);
                    }
                    else if (lines[j].GetType() == typeof(int))
                    {
                        contant = lines[j].ToString();
                    }
                    else
                    {
                        contant = Convert.ToString(lines[j]);
                    }

                    //   Console.WriteLine(lines.lines[j].GetType()+"=type,vallue="+ contant);

                    //result += ("行号:" + lines.line + "neirong=" + lines.lines[j]);
                    if (i == 0)
                    {

                        dt.Columns.Add(contant + "", typeof(string));
                    }
                    if (i > 0)
                    {
                        row.Add(contant);
                    }
                }

                if (i > 0)
                {
                    dt.Rows.Add(row.ToArray());
                }

            }
            return dt;
        }

        public static FromCellList _2ArrToFromCellList()
        {
            //交错数组
            int[][] arr = new int[2][]
            {
                new int[]  { 1, 2, 3, 5, 6 },
                new int[]  { 1, 2, 3, 4, 5 },
            };
            //交错数组
            object[][] object_arr = new object[2][]
            {
                new object[]  { 1, 2, 3, 5, 6 },
                new object[]  { 1, 2, 3, 4, 5 },
            };


            int[,] Arr = new int[2, 5] { { 1, 2, 3, 5, 6 }, { 1, 2, 3, 4, 5 } };

            object[,] objectArr = new object[2, 5] { { 1, 2, 3, 5, 6 }, { 1, 2, 3, 4, 5 } };


            int[,] a = new int[3, 4]
            {
             {0, 1, 2, 3} ,   /*  初始化索引号为 0 的行 */
             {4, 5, 6, 7} ,   /*  初始化索引号为 1 的行 */
             {8, 9, 10, 11}   /*  初始化索引号为 2 的行 */
            };


            var t = new FromCellList(object_arr);

            return t;

        }

        public static FromCellList _2ListToFromCellList()
        {
            List<List<object>> zip2List = new List<List<object>>
            {
                new List<object>{"a","b","c","d"},
                new List<object>{"1","2","3","4"},
                new List<object>{"A","B","C","D"},
                new List<object>{"一","二","三","四"},
            };
            var t = new FromCellList(zip2List);
            return t;
        }

        public static FromCellList FromCellListToFromCellList()
        {
            List<List<object>> zip2List = new List<List<object>>
            {
                new List<object>{"a","b","c","d"},
                new List<object>{"1","2","3","4"},
                new List<object>{"A","B","C","D"},
                new List<object>{"一","二","三","四"},
            };
            var t = new FromCellList(new FromCellList(zip2List));

            return t;

        }
    }
}
