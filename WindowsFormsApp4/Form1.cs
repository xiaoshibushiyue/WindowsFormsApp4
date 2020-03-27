using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using static WindowsFormsApp4.TYBH;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        //统一编号
        string TYBH;
        public Form1()
        {
            InitializeComponent();
          
        }
        public Form1(string tybh)
        {
            InitializeComponent();
            TYBH = tybh;
        }
        
        //连接
        SQLiteConnection connection = new SQLiteConnection(@"datasource=D:\石家庄\print_zhuzhuangtu\hhht.db");
        
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
          
        }
        #region 打开/关闭数据库
        /// <summary>
        /// open sqllite,but not close it
        /// </summary>
        public void open()
        {
            
            connection.Open();
           
        }

        public void close()
        {
            connection.Close();
        }
        #endregion
        #region 执行图段1sql,并转换
        /// <summary>
        /// SELECT `XH`, `CDBG`,`CDSD`,`DCHD` FROM  `MSJB_ZJ_DCYX` 
        /// </summary>
        /// <returns></returns>
        public DataTable Excute1()
        {
            
            string ExcuteSql = "SELECT `XH`, `CDBG`,`CDSD`,`DCHD` FROM  `MSJB_ZJ_DCYX` WHERE `TYBH`=@TYBH";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql,connection);
            command.Parameters.AddWithValue("@TYBH",TYBH);

            SQLiteDataReader reader = command.ExecuteReader();
          
                int num;
                DataTable table = new DataTable();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    DataColumn column = new DataColumn();
                    column.DataType = reader.GetFieldType(num);
                    column.ColumnName = reader.GetName(num);
                    table.Columns.Add(column);
                }
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    for (num = 0; num < reader.FieldCount; num++)
                    {
                        row[num] = reader[num].ToString();
                    }
                    table.Rows.Add(row);
                    row = null;
                }
            reader.Close();
            return table;
            
              
            



            //SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter();
            //DataTable dataTable = new DataTable();
            //dataAdapter.SelectCommand = command;
            //dataAdapter.Fill(dataTable);





        }
        #endregion

        #region 执行图段2sql,并转换
        /// <summary>
        /// SELECT `DCHD`,`YXMS` FROM  `MSJB_ZJ_DCYX`
        /// </summary>
        /// <returns></returns>
        public DataTable Excute2()
        {
            string ExcuteSql = "SELECT `DCHD`,`YXMS` FROM  `MSJB_ZJ_DCYX` WHERE `TYBH`=@TYBH";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH",TYBH);
            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }


        #endregion
        #region 执行图例查询，返回
        /// <summary>
        /// SELECT `YTMC` ,`TYBH` FROM  `MSJB_ZJ_DCYX`
        /// </summary>
        /// <returns></returns>
        public DataTable Excute3()
        {
            string ExcuteSql = "SELECT `YTMC` ,`TYBH` FROM  `MSJB_ZJ_DCYX` WHERE `TYBH`=@TYBH";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH",TYBH);
            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }
        #endregion

        #region 执行图段3止水-封闭sql ,并转化
        /// <summary>
        /// SELECT `QSSD`,`ZZSD`,`ZSCL`,`TYBH` FROM  `MSJB_ZJ_ZSJG` 
        /// </summary>
        /// <returns></returns>
        public DataTable Excute4()
        {
            string ExcuteSql = "SELECT `QSSD`,`ZZSD`,`ZSCL`,`TYBH` FROM  `MSJB_ZJ_ZSJG` WHERE `TYBH`=@TYBH";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH", TYBH);
            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }


        #endregion
        #region 查询管体，并执行
        /// <summary>
        /// SELECT `JGLX`,`JGNJ`,`GDSD`,`JGCD` FROM  `MSJB_ZJ_JGJG`
        /// </summary>
        /// <returns></returns>
        public DataTable Excute5()
        {
            //string QiaoShi = "桥式滤水管";
            string ExcuteSql = "SELECT `JGLX`,`JGNJ`,`GDSD`,`JGCD` FROM  `MSJB_ZJ_JGJG` WHERE `TYBH`=@TYBH " ;
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH", TYBH);
            //command.Parameters.AddWithValue("@QiaoShi",QiaoShi);
            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }
        #endregion
        #region 查询外径
        /// <summary>
        /// SELECT `ZKZJ` FROM  `MSJB_ZJ_KJJG` WHERE `
        /// </summary>
        /// <returns></returns>
        public DataTable Excute6()
        {
            //string QiaoShi = "桥式滤水管";
            string ExcuteSql = "SELECT `ZKZJ` FROM  `MSJB_ZJ_KJJG` WHERE `TYBH`=@TYBH ";
            SQLiteCommand command = new SQLiteCommand(ExcuteSql, connection);
            command.Parameters.AddWithValue("@TYBH", TYBH);
            //command.Parameters.AddWithValue("@QiaoShi",QiaoShi);
            SQLiteDataReader reader = command.ExecuteReader();

            int num;
            DataTable table = new DataTable();
            for (num = 0; num < reader.FieldCount; num++)
            {
                DataColumn column = new DataColumn();
                column.DataType = reader.GetFieldType(num);
                column.ColumnName = reader.GetName(num);
                table.Columns.Add(column);
            }
            while (reader.Read())
            {
                DataRow row = table.NewRow();
                for (num = 0; num < reader.FieldCount; num++)
                {
                    row[num] = reader[num].ToString();
                }
                table.Rows.Add(row);
                row = null;
            }
            reader.Close();
            return table;
        }

        #endregion


        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
           
            Graphics graphics = e.Graphics;
            

            DoubleBuffered = true;
            #region 图段1打印
            //设置图段1起始打印点
            int startx = 25;
            int starty = 25;

            Pen pen = new Pen(Color.Black);
            //抗锯齿呈现

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            #region 画表头
            //画表头：注意每行位置的对应
            graphics.DrawLine(pen, startx, starty, 1200 - 50, 25);
            graphics.DrawLine(pen, startx, starty + 75, 1200 - 50, starty + 75);//间隔75

            graphics.DrawLine(pen, 25, 25, 25, 100);
            //间隔45--字体，格式都在这里
            graphics.DrawLine(pen, 70, 25, 70, 100);
            Font font1 = new Font("楷体", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            PointF pointF = new PointF(43, 25);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString("地层时代及成因", font1, solidBrush, pointF, stringFormat);
            //间隔45
            graphics.DrawLine(pen, 115, 25, 115, 100);
            PointF pointF2 = new PointF(86, 25);
            graphics.DrawString("岩土层编号", font1, solidBrush, pointF2, stringFormat);
            //间隔45
            graphics.DrawLine(pen, 160, 25, 160, 100);
            PointF pointF3 = new PointF(132.5f, 25);
            graphics.DrawString("层底标高（m）", font1, solidBrush, pointF3, stringFormat);
            //间隔45
            graphics.DrawLine(pen, 205, 25, 205, 100);
            PointF pointF4 = new PointF(177.5f, 25);
            graphics.DrawString("层底深度(m)", font1, solidBrush, pointF4, stringFormat);
            //间隔45
            graphics.DrawLine(pen, 250, 25, 250, 100);
            PointF pointF5 = new PointF(222.5f, 25);
            graphics.DrawString("单层厚度(m)", font1, solidBrush, pointF5, stringFormat);
            //间隔180
            graphics.DrawLine(pen, 430, 25, 430, 100);
            PointF pointF6 = new PointF(290, 55);
            graphics.DrawString("钻孔结构及底层柱状图", font1, solidBrush, pointF6);

            //间隔720
            graphics.DrawLine(pen, 1150, 25, 1150, 100);
            PointF pointF7 = new PointF(680, 55);
            graphics.DrawString("岩       性         描         述", font1, solidBrush, pointF7);


            #endregion
            //表体打印初始位置
            int Pointx = 70;
            float Pointy = 100;
            //单层厚度单位像素
            int MinMonolayer = 29;

            //打开数据库
            open();
            //获取内存表
            DataTable table = Excute1();

            float formHeight = 0f;
            //设置图片窗体大小
            for (int rows = 0; rows < table.Rows.Count; rows++)
            {
                float a = Convert.ToSingle(table.Rows[rows][3]);
                formHeight += Pointy + a * MinMonolayer;
            }
            this.pictureBox1.Size = new Size(1200,Convert.ToInt32(formHeight));
            //打印左部分表格和内容

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //打印行线
                float a = Convert.ToSingle(table.Rows[i][3]);
                Pointy = Pointy + a * MinMonolayer;


                if (i < table.Rows.Count - 1)
                {
                    graphics.DrawLine(pen, Pointx, Pointy, 310, Pointy);
                    for (int k = 0; k < table.Columns.Count; k++)
                    {

                        //定义打印点坐标
                        float Printx;
                        float Printy;

                        Printx = 70 + k * 45 + 10;
                        Printy = Pointy - a * MinMonolayer * 0.67f;
                        PointF point = new PointF(Printx, Printy);
                        graphics.DrawString(table.Rows[i][k].ToString(), font1, solidBrush, point);

                    }

                }
                else
                {

                    graphics.DrawLine(pen, 25, Pointy, 310, Pointy);
                    for (int k = 0; k < table.Columns.Count; k++)
                    {

                        //定义打印点坐标
                        float Printx;
                        float Printy;

                        Printx = 70 + k * 45 + 10;
                        Printy = Pointy - a * MinMonolayer * 0.67f;
                        PointF point = new PointF(Printx, Printy);
                        graphics.DrawString(table.Rows[i][k].ToString(), font1, solidBrush, point);
                    }
                }

                //打印列线
                for (int j = 0; j < 7; j++)
                {
                    if (j != 6)
                    {
                        graphics.DrawLine(pen, 25 + j * 45, 100, 25 + j * 45, Pointy);
                    }
                    else
                    {
                        graphics.DrawLine(pen, 25 + 5 * 45 + 60, 100, 25 + 5 * 45 + 60, Pointy);
                    }
                }


            }
            //不释放卡界面,但是释放了就不能将参数传过去
            //graphics.Dispose();
            //pen.Dispose();
            //solidBrush.Dispose();


            
            #endregion
            #region 图段2打印
            //设置图段2打印起始点
            int section2startX = 370;
            int section2starty = 100;

           
            DataTable table2 = Excute2();
            //打印图段2行线
            float pointy2 = section2starty;

            for (int i=0;i<table2.Rows.Count;i++)
            {
                pointy2 = pointy2 + (Convert.ToSingle(table2.Rows[i][0])*MinMonolayer);
                graphics.DrawLine(pen,370,pointy2, 1150,pointy2);

              //文字格式缩进
                PointF point2 = new PointF(470,pointy2- (Convert.ToSingle(table2.Rows[i][0]) * MinMonolayer)/2);
                graphics.DrawString(table2.Rows[i][1].ToString(),font1,solidBrush,point2);

                //打印图例
                DataTable table3 = Excute3();
                string Legend = table3.Rows[i][0].ToString();
                string path = Query(Legend);
                Image image = new Bitmap(path);
                float paint = Convert.ToSingle(MinMonolayer)*Convert.ToSingle(table2.Rows[i][0]);
                //图例-可以用clone，但是这个图像只能用宽为60像素的，长不能小了
                graphics.DrawImage(image, new RectangleF(370+1, pointy2 +1- (Convert.ToSingle(table2.Rows[i][0]) * MinMonolayer), 60-1, paint-2),new RectangleF(0,0,image.Width, paint-1),GraphicsUnit.Pixel);
                graphics.DrawImage(image, new RectangleF(250 + 1, pointy2 + 1 - (Convert.ToSingle(table2.Rows[i][0]) * MinMonolayer), 60 - 1, paint - 2), new RectangleF(0, 0, image.Width, paint - 1), GraphicsUnit.Pixel);

             

               
            }

            //打印图段2列线
            graphics.DrawLine(pen,370,100,370,pointy2);
            graphics.DrawLine(pen, 430, 100, 430, pointy2);
            graphics.DrawLine(pen,1150,100,1150,pointy2);



           
            #endregion

            #region 打印图段3
            //画表格
            graphics.DrawLine(pen, 320, 100, 320, pointy2);
            graphics.DrawLine(pen, 360, 100, 360, pointy2);
            graphics.DrawLine(pen, 310, pointy2, 370, pointy2);
            //输入内容
            DataTable table4 = Excute4();
            //定义画刷
            SolidBrush brush = new SolidBrush(Color.Black);
            //定义止水的打印位置
            float ZhishuiY = Convert.ToSingle(table.Rows[0][1])+Convert.ToSingle(table.Rows[0][3]);
            float ZhishuistartY = Convert.ToSingle(table4.Rows[0][0]);
            float ZhishuiendY = Convert.ToSingle(table4.Rows[0][1]);

            //定义止水打印的坐标
            float Zhishuiprinty = ZhishuiY - ZhishuistartY;
            Image imageZS = new Bitmap(ZSMaterial(table4));
            TextureBrush textureBrushZS = new TextureBrush(imageZS);
            graphics.FillRectangle(textureBrushZS, 310, Zhishuiprinty * MinMonolayer + 100,10,(ZhishuistartY-ZhishuiendY)*MinMonolayer);//止水的材料还没有考虑
            graphics.FillRectangle(textureBrushZS, 360, Zhishuiprinty * MinMonolayer + 100, 10, (ZhishuistartY - ZhishuiendY) * MinMonolayer);
           
            

            //打印其他封闭
            //打印粘土封闭
            
            Image imageNianTu = new Bitmap(@"../../picture/粘土封闭.png");
            TextureBrush textureBrushNT = new TextureBrush(imageNianTu);
            graphics.FillRectangle(textureBrushNT, new RectangleF(310, 100, 10, (ZhishuiY - ZhishuistartY) * MinMonolayer));
            graphics.FillRectangle(textureBrushNT, new RectangleF(360, 100, 10, (ZhishuiY - ZhishuistartY) * MinMonolayer));

            //打印砾料封闭
            Image imageLiLiao = new Bitmap(@"../../picture/砾料.png");
            float LishiY = Convert.ToSingle(table.Rows[table.Rows.Count-1][1]);
           
            TextureBrush textureBrushLL = new TextureBrush(imageLiLiao);
            graphics.FillRectangle(textureBrushLL,new RectangleF(310, 100 + Zhishuiprinty * MinMonolayer + (ZhishuistartY - ZhishuiendY) * MinMonolayer, 10, (ZhishuiendY - LishiY) * MinMonolayer));
            graphics.FillRectangle(textureBrushLL, new RectangleF(360, 100 + Zhishuiprinty * MinMonolayer + (ZhishuistartY - ZhishuiendY) * MinMonolayer, 10, (ZhishuiendY - LishiY) * MinMonolayer));



            //打印桥式滤水管
            DataTable table5 = Excute5();
            float QiaoSD = Convert.ToSingle(table5.Rows[0][2]);
            float QiaoCD = Convert.ToSingle(table5.Rows[0][3]);
            graphics.DrawLine(pen,320,100+MinMonolayer*Convert.ToSingle(table5.Rows[0][3]),360, 100 + MinMonolayer * Convert.ToSingle(table5.Rows[0][3]));
            graphics.DrawLine(pen,320, 100+MinMonolayer * (Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3])), 360, 100+MinMonolayer * (Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3])));
            
            Image imageQL = new Bitmap(@"..\..\picture\填土.png");
            TextureBrush textureBrush = new TextureBrush(imageQL);
            graphics.FillRectangle(textureBrush,new RectangleF(320, 100+ MinMonolayer * Convert.ToSingle(table5.Rows[0][3]),40, MinMonolayer * Convert.ToSingle(table5.Rows[1][3])));

            //打印字体
            string ShiGuan = "实管";
            string LvShuiGuan = "桥式滤水管";
            Font font2 = new Font("楷体", 11, FontStyle.Bold, GraphicsUnit.Pixel);
            graphics.DrawString("0-"+Convert.ToString(table5.Rows[0][3])+"米", font2, brush,320, 100 -50 + MinMonolayer * Convert.ToSingle(table5.Rows[0][3]));
            graphics.DrawString(ShiGuan, font2, brush,320, 100 -40 + MinMonolayer * Convert.ToSingle(table5.Rows[0][3]));

            graphics.DrawString(Convert.ToString(table5.Rows[0][3])+"-"+Convert.ToString(Convert.ToSingle(table5.Rows[0][3])+Convert.ToSingle(table5.Rows[1][3]))+"米", font2, brush,320, 100 +50 + MinMonolayer * Convert.ToSingle(table5.Rows[0][3]));
            graphics.DrawString(LvShuiGuan, font2, brush,335, 100 + 60 + MinMonolayer * Convert.ToSingle(table5.Rows[0][3]),stringFormat);

            graphics.DrawString(Convert.ToString(Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3]))+"-"+ Convert.ToString(Convert.ToSingle(table5.Rows[0][3]) +Convert.ToSingle(table5.Rows[2][3])+ Convert.ToSingle(table5.Rows[1][3]))+"米", font2, brush,320, 100 -30+ MinMonolayer * (Convert.ToSingle(table5.Rows[0][3])  +Convert.ToSingle(table5.Rows[1][3])+Convert.ToSingle(table5.Rows[2][3]) ));
            graphics.DrawString(ShiGuan, font2, brush,320, 100 - 20 + MinMonolayer * (Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3]) + Convert.ToSingle(table5.Rows[2][3]))  );

            string NianTuFengBi = "粘土封闭";
            graphics.DrawString(NianTuFengBi,font2,brush,330,100+10,stringFormat);
            graphics.DrawString(Convert.ToString(ZhishuiY-ZhishuistartY)+"米",font2,brush,330,100+60);


            string NianTuQiuZhiShui = "粘土球止水";
            graphics.DrawString(NianTuQiuZhiShui,font2,brush,330, Zhishuiprinty * MinMonolayer + 100,stringFormat);
            graphics.DrawString(Convert.ToString(ZhishuistartY-ZhishuiendY)+"米",font2,brush,330, Zhishuiprinty * MinMonolayer + 100+60);
            string LiLiao = "砾料";
            graphics.DrawString(LiLiao,font2,brush,320, 100 - 50 + MinMonolayer * (Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3]) + Convert.ToSingle(table5.Rows[2][3]))  );
            graphics.DrawString(Convert.ToString(ZhishuiY-ZhishuiendY)+"米",font2,brush,320, 100 - 40 + MinMonolayer * (Convert.ToSingle(table5.Rows[0][3]) + Convert.ToSingle(table5.Rows[1][3]) + Convert.ToSingle(table5.Rows[2][3]))  );

            //打印内外径
            Pen pendiameter = new Pen(Color.Black);
            pendiameter.StartCap = LineCap.ArrowAnchor;
            pendiameter.EndCap = LineCap.ArrowAnchor;
            graphics.DrawLine(pendiameter,320, Zhishuiprinty * MinMonolayer + 100-40,360, Zhishuiprinty * MinMonolayer + 100-40);
            graphics.DrawLine(pendiameter,310, Zhishuiprinty * MinMonolayer + 100-20,370, Zhishuiprinty * MinMonolayer + 100-20);
            graphics.DrawString(Convert.ToString(table5.Rows[0][1])+"mm",font2,brush,325, Zhishuiprinty * MinMonolayer + 100 - 50);
            DataTable table6 = Excute6();

            graphics.DrawString(Convert.ToString(table6.Rows[0][0])+"mm",font2,brush,325, Zhishuiprinty * MinMonolayer + 100 - 35);
            #endregion


            close();
        }

        #region 判断止水材料
        public  string ZSMaterial(DataTable table)
        {
            string path = "";
            if (Convert.ToString(table.Rows[0][2])=="止水球")
            {
                path = "../../picture/止水球.png";
                return path;
            }
            return path;
        }
        #endregion


        #region 判断图例
        public string Query(string a)
        {
            string path="";
            //在填土这里由一个素填土和杂填土，而且图例里面没有
            if (a == "素填土")
            {
                path = @"../../picture/填土.png";
                return path;
            }
            else if (a=="杂填土")
            {
                path = @"../../picture/填土.png";
                return path;

            }
            else if (a == "黏质砂土")
            {
                path = @"../../picture/黏质沙土.png";
                return path;
            }
            else if (a == "砂质粘土")
            {
                path = @"../../picture/砂质黏土.png";
                return path;
            }
            else if (a == "卵石")
            {
                path = @"../../picture/卵石小.png";
                return path;
            }
            else if (a == "粘土")
            {
                path = @"../../picture/黏土.png";
                return path;
            }
            else if (a == "卵砾石")
            {
                path = @"../../picture/卵砾石.png";
                return path;
            }
            else if (a == "砾石")
            {
                path = @"../../picture/砾石.png";
                return path;

            }
            else if (a == "砂砾石")
            {
                path = @"../../picture\砂砾石.png";
                return path;
            }
            else if (a == "粗砂")
            {
                path = @"../../picture/粗砂.png";
                return path;
            }
            else if (a == "中砂")
            {
                path = @"../../picture/中砂.png";
                return path;
            }
            else if (a == "细砂")
            {
                path = @"../../picture/细砂.png";
                return path;
            }
            else if (a == "粉砂")
            {
                path = @"../../picture/粉砂.png";
                return path;
            }
            else if (a == "淤泥质粉土")
            {
                path = @"../../picture/淤泥质粉土.png";
                return path;
            }
            else if (a == "黄土")
            {
                path = @"../../picture/黄土.png";
                return path;
            }
            else if (a == "石灰岩")
            {
                path = @"../../picture/石灰岩.png";
                return path;
            }
            else if (a == "白云质灰岩")
            {
                path = @"../../picture/白云质灰岩.png";
                return path;
            }
            else if (a == "泥质灰岩")
            {
                path = @"../../picture/泥质灰岩.png";
                return path;
            }
            else if (a == "角砾状灰岩")
            {
                path = @"../../picture/角砾状灰岩.png";
                return path;
            }
            else if (a == "砂砾岩")
            {
                path = @"../../picture/砂砾岩.png";
                return path;
            }
            else if (a == "砂岩")
            {
                path = @"../../picture/砂岩.png";
                return path;
            }
            else if (a == "钙质砂岩")
            {
                path = @"../../picture/钙质砂岩.png";
                return path;
            }
            else if (a == "泥质砂岩")
            {
                path = @"../../picture/泥质砂岩.png";
                return path;
            }
            else if (a == "泥岩")
            {
                path = @"../../picture/泥岩.png";
                return path;
            }
            else if (a == "页岩")
            {
                path = @"../../picture/页岩.png";
                return path;
            }
            else if (a == "大理岩")
            {
                path = @"../../picture/大理岩.png";
                return path;
            }
            else if (a == "板岩")
            {
                path = @"../../picture/板岩.png";
                return path;
            }
            else if (a == "片岩")
            {
                path = @"../../picture/片岩.png";
                return path;
            }
            else if (a == "片麻岩")
            {
                path = @"../../picture/片麻岩.png";
                return path;
            }
            else if (a == "玄武岩")
            {
                path = @"../../picture/玄武岩.png";
                return path;
            }
            else if (a == "流纹岩")
            {
                path = @"../../picture/流纹岩.png";
                return path;
            }
            else if (a == "安山岩")
            {
                path = @"../../picture/安山岩.png";
                return path;
            }
            else if (a == "火山角砾岩")
            {
                path = @"../../picture/火山角砾岩.png";
                return path;
            }
            else if (a == "花岗岩")
            {
                path = @"../../picture/花岗岩.png";
                return path;
            }
            else if (a == "闪长岩")
            {
                path = @"../../picture/闪长岩.png";
                return path;
            }
            else if (a == "橄榄岩")
            {
                path = @"../../picture/橄榄岩.png";
                return path;
            }

            return null;
            //for (int i = 0; i < a.Rows.Count; i++)
            //{
            //    if (a.Rows[i][0].ToString() == "素填土")
            //    {
            //         path = @"D:\vs 2017\WindowsFormsApp4\WindowsFormsApp4\picture\tiantu.png";
            //        return path;
            //    }
            //    else if (a.Rows[i][0].ToString() == "粘质砂土")
            //    {
            //         path = @"D:\vs 2017\WindowsFormsApp4\WindowsFormsApp4\picture\nianzhishatu.PNG";
            //        return path;

            //    }
            //    else if (a.Rows[i][0].ToString() == "砂质粘土")
            //    {
            //         path = @"D:\vs 2017\WindowsFormsApp4\WindowsFormsApp4\picture\shazhiniantu.PNG";
            //        return path;


            //    }
            //    else if (a.Rows[i][0].ToString() == "卵石")
            //    {
            //         path = @"D:\vs 2017\WindowsFormsApp4\WindowsFormsApp4\picture\luanshi.PNG";
            //        return path;
            //    }
            //    else if (a.Rows[i][0].ToString() == "粘土")
            //    {
            //        path = @"D:\vs 2017\WindowsFormsApp4\WindowsFormsApp4\picture\niantu.PNG";
            //        return path;
            //    }

            //}
            //return null;
        }
       
            

        
        #endregion

        
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //测试数据
            Pen pen = new Pen(Color.Black);
            e.Graphics.DrawLine(pen,300,300,600,600);

            }

        private void 打印机打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            printDocument1.Print();
        }

        private void 打印ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

            pictureBox1.Dock = DockStyle.None;
            pictureBox1.BackColor = Color.White;
            
            printDialog1.Document = printDocument1;
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.pictureBox1.Size = new Size(1200, 2600);
            //Dispose(); 释放的画一闪就没了

            
        }
    }
}
