using System;
using System.Data;
using Hydrologic.Core;
using Hydrologic.Database;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.Caching;
using System.Threading;

namespace HydrologicSystem.Module.SurveyPoint.Plugin.Modules.ZTDCD
{

    public class PaintZZT

    {
        private static PaintZZT instance = new PaintZZT();
        private PaintZZT() { }
        public static PaintZZT getInstance() { return instance; }
        public  string TYBH { get;internal set; }
        Bitmap bitmap;
        /// <summary>
        /// 打印函数，这里读取的数据并没有从数据库进行读取，而是在系统界面的表格里面读的。
        /// </summary>
        /// <returns></returns>
        public Bitmap print(DataTable table_DCYX,DataTable tableZSJG,DataTable tableJGJG,DataTable table_KJJG)
        {
            
            //打印比例(单层厚度所占的像素)
            //120是每米代指的像素大小
            int MinMonolayer = 120;
            
            #region 定义图片大小
            //这里是叠加高度（米），用来计算图片总的高度
            float PlusHeight = 0;
            for (int i = 0; i < table_DCYX.Rows.Count; i++)
            {
                PlusHeight += Convert.ToSingle(table_DCYX.Rows[i][3]);
            }
            //这里是总高度对应的像素，470是图片的表头的高度
            int HeightPix = MinMonolayer * (Convert.ToInt32(PlusHeight) + 1) + 470;
            //图片的整体大小（像素为单位）
            bitmap= new Bitmap(2600,HeightPix);
            Graphics graphics = Graphics.FromImage(bitmap);
            //闪烁
            //control.DoubleBuffered = true;
            //graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            #endregion

            #region 打印表头
            Pen pen = new Pen(Color.Black);
            Pen pen_cap = new Pen(Color.Black);
            pen_cap.EndCap = LineCap.ArrowAnchor;
            Pen pen_doublecap = new Pen(Color.Black);
            pen_doublecap.StartCap = LineCap.ArrowAnchor;
            pen_doublecap.EndCap = LineCap.ArrowAnchor;

            graphics.DrawLine(pen, 10, 10, 2590, 10);
            graphics.DrawLine(pen, 10, 480, 2590, 480);
            //定义文字格式
            Font font = new Font("楷体", 50, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            //打印表头文字及竖线
            float HeightOfTL = MinMonolayer * PlusHeight + 480;
            graphics.DrawLine(pen, 10, 10, 10, HeightOfTL);
            graphics.DrawLine(pen, 125, 10, 125, HeightOfTL);
            PointF point1 = new PointF(30, 30);
            graphics.DrawString("地层时代及成因", font, solidBrush, point1, stringFormat);
            graphics.DrawLine(pen, 240, 10, 240, HeightOfTL);
            PointF point2 = new PointF(145, 30);
            graphics.DrawString("岩土层编号", font, solidBrush, point2, stringFormat);
            graphics.DrawLine(pen, 355, 10, 355, HeightOfTL);
            graphics.DrawString("层底标高(m)", font, solidBrush, 260, 30, stringFormat);
            graphics.DrawLine(pen, 470, 10, 470, HeightOfTL);
            graphics.DrawString("层底深度(m)", font, solidBrush, 375, 30, stringFormat);
            graphics.DrawLine(pen, 585, 10, 585, HeightOfTL);
            graphics.DrawString("单层厚度(m)", font, solidBrush, 490, 30, stringFormat);
            graphics.DrawLine(pen, 1000, 10, 1000, HeightOfTL);
            graphics.DrawString("钻孔结构及", font, solidBrush, 650, 150);
            graphics.DrawString("地质柱状图", font, solidBrush, 650, 200);
            //打印比例
            graphics.DrawString("1:" + 100, font, solidBrush, 700, 250);
            graphics.DrawLine(pen, 2590, 10, 2590, HeightOfTL);
            graphics.DrawString("岩          性            描          述", font, solidBrush, 1200, 200);
            #endregion
            #region 打印其他表格线（钻孔结构及地质柱状图下面）
            graphics.DrawLine(pen, 725, 480, 725, HeightOfTL);
            graphics.DrawLine(pen, 750, 480, 750, HeightOfTL);
            graphics.DrawLine(pen, 835, 480, 835, HeightOfTL);
            graphics.DrawLine(pen, 860, 480, 860, HeightOfTL);
            graphics.DrawLine(pen, 1000, 480, 1000, HeightOfTL);
            //下面是打印表格横线
            int pointx = 125;
            float pointy = 480;
            for (int i = 0; i < table_DCYX.Rows.Count; i++)
            {
                pointy += MinMonolayer * (Convert.ToSingle(table_DCYX.Rows[i][3]));
                graphics.DrawLine(pen, pointx, pointy, 2590, pointy);
            }
            //从定义的图片（10，10）处开始绘制图片
            graphics.DrawLine(pen, 10, pointy, 125, pointy);

            #endregion
            #region 打印表格内容
            float pointy2 = 480;
            //定义打印字体
            Font font_ZSJG = new Font("楷体", 25, FontStyle.Regular, GraphicsUnit.Pixel);
            //打印地层及填充岩芯
            for (int i = 0; i < table_DCYX.Rows.Count; i++)
            {
                int pointx1 = 125;

                for (int j = 0; j < table_DCYX.Columns.Count; j++)
                {
                    if (j < table_DCYX.Columns.Count - 2)
                    {
                        string a = Convert.ToString(table_DCYX.Rows[i][j]);
                        graphics.DrawString(a, font_ZSJG, solidBrush, pointx1, pointy2 + (MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3])) / 3);
                        pointx1 += 115;
                    }

                    if (j == table_DCYX.Columns.Count - 1)
                    {
                        string b = Convert.ToString(table_DCYX.Rows[i][j]);
                        RectangleF rectF1 = new RectangleF(1000, pointy2 + (MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3])) / 3, 1580, MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3]));
                        
                        graphics.DrawString(b, font_ZSJG, solidBrush, rectF1);
                    }

                }
                try
                {
                    Image image_TuLi = new Bitmap(QueryLegend(Convert.ToString(table_DCYX.Rows[i][4])));
                    TextureBrush textureBrush = new TextureBrush(image_TuLi);
                    graphics.FillRectangle(textureBrush, new RectangleF(585, pointy2, 140, MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3])));
                    graphics.FillRectangle(textureBrush, new RectangleF(860, pointy2, 140, MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3])));
                    textureBrush.Dispose();
                    pointy2 += MinMonolayer * Convert.ToSingle(table_DCYX.Rows[i][3]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("您输入的岩土类型有误");
                }


            }
            //打印封闭
            SolidBrush brushfill = new SolidBrush(Color.White);//这个白色画笔，作用是填充
            Dictionary<int, float> point_of_FB = new Dictionary<int, float>();
            float Height_of_ground = Convert.ToSingle(table_DCYX.Rows[0][1]) + Convert.ToSingle(table_DCYX.Rows[0][3]);

            if (tableZSJG.Rows.Count >= 1)
            {
                //计算地面高程
                for (int i=0;i<tableZSJG.Rows.Count;i++)
                {
                    
                    using (Bitmap bitmap_ZS=new Bitmap(ZSMaterial(tableZSJG.Rows[i][2].ToString())))
                    {
                        float y = 0f;
                        float height()
                        {
                            int j = i;
                            if (j>0)
                            {
                                y=MinMonolayer*(Height_of_ground - Convert.ToSingle(tableZSJG.Rows[j][1]))- MinMonolayer*(Height_of_ground - Convert.ToSingle(tableZSJG.Rows[j-1][1]));
                                return y;
                            }
                            return MinMonolayer*(Height_of_ground - Convert.ToSingle(tableZSJG.Rows[j][1]));
                        }
                        
                        point_of_FB.Add(i,height());

                        TextureBrush textureBrushFB = new TextureBrush(bitmap_ZS);
                        graphics.FillRectangle(brushfill, new RectangleF(725, 480 + MinMonolayer*(Height_of_ground-Convert.ToSingle(tableZSJG.Rows[i][0])), 25, height()));
                        graphics.FillRectangle(textureBrushFB, new RectangleF(725, 480 + MinMonolayer * (Height_of_ground - Convert.ToSingle(tableZSJG.Rows[i][0])), 25, height()));
                        graphics.FillRectangle(brushfill, new RectangleF(835, 480 + MinMonolayer * (Height_of_ground - Convert.ToSingle(tableZSJG.Rows[i][0])), 25, height()));
                        graphics.FillRectangle(textureBrushFB, new RectangleF(835, 480 + MinMonolayer * (Height_of_ground - Convert.ToSingle(tableZSJG.Rows[i][0])), 25, height()));

                    }
                }
                
            }
            else
            {
                MessageBox.Show("表信息不全，无止水结构");
            }
            //打印管体
            Dictionary<int, float> point_of_QSPipe = new Dictionary<int, float>();
            Dictionary<int, float> point_of_Pipe = new Dictionary<int, float>();

            if (tableJGJG.Rows.Count >= 1)
            {
                for (int i=0;i<tableJGJG.Rows.Count;i++)
                {
                    using (Bitmap bitmap_pipe = new Bitmap(pipe_type(tableJGJG.Rows[i][0].ToString())))
                    {
                        TextureBrush textureBrushLSG = new TextureBrush(bitmap_pipe);
                        if (i==0)
                        {
                            graphics.FillRectangle(textureBrushLSG, new RectangleF(750, 480, 85, MinMonolayer * Convert.ToSingle(tableJGJG.Rows[i][3])));
                            point_of_Pipe.Add(i, 480);
                        }
                        else
                        {
                            float y = 480;
                            int j = i;
                            float add()
                            {
                                
                                if (j>0)
                                {
                                    y += MinMonolayer * (Convert.ToSingle(tableJGJG.Rows[j - 1][3]));
                                    j--;
                                    return add();

                                }
                                return y;
                            }
                           
                            graphics.FillRectangle(textureBrushLSG, new RectangleF(750, add(), 85, MinMonolayer * Convert.ToSingle(tableJGJG.Rows[i][3])));
                            if (tableJGJG.Rows[i][0].ToString()== "桥式滤水管")
                            {
                                point_of_QSPipe.Add(i, add());

                                graphics.DrawLine(pen, 750, add(), 750+85, add());
                                graphics.DrawLine(pen, 750, add() + MinMonolayer * Convert.ToSingle(tableJGJG.Rows[i][3]), 750+85, add() + MinMonolayer * Convert.ToSingle(tableJGJG.Rows[i][3]));

                            }
                            point_of_Pipe.Add(i,add());
                        }

                    }

                }
                //打印中心文字
                RectangleF[] rectangleFQS = new RectangleF[tableJGJG.Rows.Count];
                //定义桥式滤水管区域
                for (int i=0;i<tableJGJG.Rows.Count;i++)
                {
                    if (i > 0 && i < tableJGJG.Rows.Count - 1)
                    {
                        if (tableJGJG.Rows[i][0].ToString()=="桥式滤水管")
                        {
                            rectangleFQS[i] = new RectangleF(750, point_of_QSPipe[i],85,Convert.ToSingle(tableJGJG.Rows[i][3]));

                        }
                    }
                }
                RectangleF[] rectangleFNTZSQ = new RectangleF[tableZSJG.Rows.Count];
                //定义止水球区域
                for (int i=0;i<tableZSJG.Rows.Count;i++)
                {
                    if (tableZSJG.Rows[i][2].ToString()=="粘土球")
                    {
                        rectangleFNTZSQ[i] = new RectangleF(750, 480 + MinMonolayer * (Height_of_ground - Convert.ToSingle(tableZSJG.Rows[i][0])), 85, point_of_FB[i]);
                    }
                }
                RectangleF[] rectangleFFB = new RectangleF[tableZSJG.Rows.Count];
                //定义封闭区域
                for (int i=0;i<tableZSJG.Rows.Count;i++)
                {
                    rectangleFFB[i] = new RectangleF(750, 480 + MinMonolayer * (Height_of_ground - Convert.ToSingle(tableZSJG.Rows[i][0])),85,point_of_FB[i]);
                }
                RectangleF[] rectangleFPipe = new RectangleF[tableZSJG.Rows.Count];
                //定义整个管体区域
                for (int i=0; i < tableJGJG.Rows.Count; i++)
                {
                    
                    rectangleFPipe[i] = new RectangleF(750, point_of_Pipe[i], 85, MinMonolayer * Convert.ToSingle(tableJGJG.Rows[i][3]));
                }
                
                //记录文字区域
                Dictionary<int, RectangleF> keyValues_pipe_string = new Dictionary<int, RectangleF>();
                //打印管体文字
                
                for (int i = 0; i < tableJGJG.Rows.Count; i++)
                {
                    if (tableJGJG.Rows[i][0].ToString() == "实管")
                    {
                        keyValues_pipe_string.Add(i, GetRectangleF(rectangleFPipe[i], "实管"));
                        graphics.DrawString($"实管{Rounding(Convert.ToSingle(tableJGJG.Rows[i][3]))}米", font_ZSJG, solidBrush, GetRectangleF(rectangleFPipe[i], "实管"), stringFormat);

                    }
                    else if (tableJGJG.Rows[i][0].ToString() == "桥式滤水管")
                    {
                        keyValues_pipe_string.Add(i, GetRectangleF(rectangleFPipe[i], "桥式滤水管"));

                        graphics.DrawString($"桥式滤水管{Rounding(Convert.ToSingle(tableJGJG.Rows[i][3]))}米", font_ZSJG, solidBrush, GetRectangleF(rectangleFPipe[i], "桥式滤水管"), stringFormat);

                    }
                    else
                    {
                        graphics.DrawString($"error", font_ZSJG, solidBrush, new RectangleF(rectangleFPipe[i].X, rectangleFPipe[i].Y + rectangleFPipe[i].Height / 2, rectangleFPipe[i].Width, rectangleFPipe[i].Height / 2), stringFormat);

                    }
                }
                //打印止水结构文字(在整个区域内打印的话，会根据字体的大小和具体的起始打印位置来打印文字，并不会受区域大小影响)

                for (int i=0;i<rectangleFFB.Length;i++)
                {
                    if (tableZSJG.Rows[i][2].ToString() == "粘土")
                    {

                        RectangleF rectangle = Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i], "粘土封闭"), rectangleFFB[i]);
                        graphics.DrawString($"粘土封闭{Rounding(Convert.ToSingle(tableZSJG.Rows[i][0]) - Convert.ToSingle(tableZSJG.Rows[i][1]))}米", font_ZSJG, solidBrush, Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i], "粘土封闭"),rectangleFFB[i]) , stringFormat);
                        graphics.DrawLine(pen_cap,rectangle.X+25,rectangle.Y+rectangle.Height/2,rectangle.X+55,10+rectangle.Y+rectangle.Height/2);
                        keyValues_pipe_string.Add(i + tableJGJG.Rows.Count, GetRectangleF(rectangleFFB[i], "粘土封闭"));

                    }
                    else if (tableZSJG.Rows[i][2].ToString() == "粘土球")
                    {

                        graphics.DrawString($"粘土球止水{Rounding(Convert.ToSingle(tableZSJG.Rows[i][0]) - Convert.ToSingle(tableZSJG.Rows[i][1]))}米", font_ZSJG, solidBrush, Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i], "粘土球"), rectangleFFB[i])
, stringFormat);
                        RectangleF rectangle = Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i], "粘土球"), rectangleFFB[i]);

                        graphics.DrawLine(pen_cap, rectangle.X + 25, rectangle.Y + rectangle.Height / 2, rectangle.X + 55, 10+ rectangle.Y + rectangle.Height / 2);
                        keyValues_pipe_string.Add(i + tableJGJG.Rows.Count, GetRectangleF(rectangleFFB[i], "粘土球"));


                    }
                    else if (tableZSJG.Rows[i][2].ToString() == "砾料")
                    {
                        RectangleF rectangle = Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i], "砾料"), rectangleFFB[i]);

                        graphics.DrawString($"砾料{Rounding(Convert.ToSingle(tableZSJG.Rows[i][0]) - Convert.ToSingle(tableZSJG.Rows[i][1]))}米", font_ZSJG, solidBrush, Collision(keyValues_pipe_string, GetRectangleF(rectangleFFB[i],"砾料"),rectangleFFB[i]), stringFormat);
                        graphics.DrawLine(pen_cap, rectangle.X + 25, rectangle.Y + rectangle.Height / 2, rectangle.X + 55, 10+rectangle.Y + rectangle.Height / 2);
                        keyValues_pipe_string.Add(i + tableJGJG.Rows.Count, GetRectangleF(rectangleFFB[i], "砾料"));

                    }
                    else
                    {
                        graphics.DrawString("数据库没有此止水结构", font_ZSJG, solidBrush, rectangleFFB[i], stringFormat);
                    }
                }
                
                {
                    //打印内外径
                    float internal_diameter = Convert.ToSingle(tableJGJG.Rows[0][1]);
                    float external_diameter = Convert.ToSingle(table_KJJG.Rows[0][0]);
                    
                    RectangleF rectangle_internal = new RectangleF(750, generate_Y(),85,50);
                    Thread.Sleep(100);//防止生成的位置一样
                    RectangleF rectangle_external = new RectangleF(750, generate_Y(), 85, 50);
                    int generate_Y()
                    {
                        Random random = new Random();
                        return random.Next(480, HeightPix);
                    }
                    RectangleF rectangle_nei = Collision(keyValues_pipe_string, rectangle_internal);
                    graphics.DrawString(internal_diameter.ToString()+"mm", font_ZSJG, solidBrush, rectangle_nei);
                    graphics.DrawLine(pen_doublecap,rectangle_nei.X,rectangle_nei.Y+25,rectangle_nei.X+85,rectangle_nei.Y+25);
                    RectangleF rectangle_wai = Collision(keyValues_pipe_string, rectangle_external);

                    graphics.DrawString(external_diameter.ToString() + "mm", font_ZSJG, solidBrush, rectangle_wai);
                    graphics.DrawLine(pen_doublecap,rectangle_wai.X-25,rectangle_wai.Y+25,rectangle_wai.X+110,rectangle_wai.Y+25);

                }
            }
            else
            {
                MessageBox.Show("表信息不全，无井管结构");
            }

            #endregion
            graphics.Dispose();
            
            return bitmap;
        }
      
        /// <summary>
        /// 查询图例,返回路径
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string QueryLegend(string a)
        {
            //这个二维图片路径和三维柱体的纹理是不一样的
            string path = System.Windows.Forms.Application.StartupPath + @"\data\picture\";
            string filename = path + string.Format("{0}.png", a);
            if (!System.IO.File.Exists(filename))
                return path + @"/空.png";
            return filename;
            
        }
        /// <summary>
        /// 查询止水材料
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string ZSMaterial(string material)
        {
            string path = System.Windows.Forms.Application.StartupPath + @"\data\picture";

            if (material == "粘土球")
            {
                path += @"\止水球.png";
                return path;
            }
            else if (material == "粘土")
            {
                path += @"\粘土封闭.png";
                return path;
            }
            else if (material == "砾料")
            {
                path += @"\砾料.png";
                return path;
            }
            else
            {
                MessageBox.Show("您输入的止水材料不存在数据库中");
            }
            return path;
        }
        /// <summary>
        /// 查询管体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string pipe_type(string type)
        {
            
            string path = System.Windows.Forms.Application.StartupPath + @"\data\picture\";
            string filename = path + string.Format("{0}.png", type);
            if (!System.IO.File.Exists(filename))
                return path + @"\空.png";
           return filename;

        }
        /// <summary>
        /// 内外径冲突检测，并返回正确的位置
        /// </summary>
        /// <param name="keyValues">所有区域</param>
        /// <param name="rectangle">内外径要绘制的区域</param>
        /// <returns></returns>
        public RectangleF Collision(Dictionary<int, RectangleF> keyValues,RectangleF rectangle)
        {
            for (int i=0;i<keyValues.Count;i++)
            {
                if (keyValues.ElementAt(i).Value.IntersectsWith(rectangle))
                {
                    rectangle=move_rectanglef(rectangle);
                    return Collision(keyValues, rectangle);
                    
                }
               
            }
            return rectangle;
            RectangleF move_rectanglef(RectangleF rectangles)
            {
                return new RectangleF(rectangles.X,rectangles.Y-2,rectangles.Width,rectangles.Height);
            }
            
        }
        /// <summary>
        /// 检测文字冲突，并处理输出合适的区域（思路：迭代检查，避免所有冲突，如果空间不足，bug）
        /// </summary>
        /// <param name="keyValues">内管所有文字区域</param>
        /// <param name="rectangleFFB_SG">外管文字区域</param>
        /// <param name="rectangleFFB">外管区域</param>
        /// <returns></returns>
        public RectangleF Collision(Dictionary<int,RectangleF> keyValues, RectangleF rectangleFFB_SG,RectangleF rectangleFFB)
        {
            for (int i=0;i<keyValues.Count;i++)
            {
                if (keyValues.ElementAt(i).Value.IntersectsWith(rectangleFFB_SG))
                {
                    RectangleF rectangleFFB_SG_clone = rectangleFFB_SG;
                    rectangleFFB_SG_clone.Intersect(keyValues.ElementAt(i).Value);
                    float overlap_height = rectangleFFB_SG_clone.Height;
                    if (rectangleFFB.Y<=rectangleFFB_SG.Y-overlap_height)
                    {
                        return move_rectanglef(rectangleFFB_SG,"up",overlap_height);
                    }
                    else if (rectangleFFB.Y+rectangleFFB.Height> rectangleFFB_SG.Y+rectangleFFB_SG.Height+keyValues.ElementAt(i).Value.Height - overlap_height+rectangleFFB_SG.Height)
                    {
                        return move_rectanglef(rectangleFFB_SG,"down", rectangleFFB_SG.Height + keyValues.ElementAt(i).Value.Height - overlap_height);
                    }
                    else
                    {
                        return rectangleFFB_SG;
                    }
                    
                }

            }
            return rectangleFFB_SG;
            //这是一个局部函数，用来上下移动区域
            RectangleF move_rectanglef(RectangleF rectangleF,string direction,float height)
            {
                if (direction=="down")
                {
                    RectangleF rectangleF_ = new RectangleF(rectangleF.X, rectangleF.Y + height, rectangleF.Width, rectangleF.Height);
                    return rectangleF_;
                }
                else if (direction=="up")
                {
                    RectangleF rectangleF_ = new RectangleF(rectangleF.X, rectangleF.Y - height, rectangleF.Width, rectangleF.Height);
                    return rectangleF_;
                }
                else
                {
                    //TODO
                }
                return rectangleF;
            }
            
        }
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Rounding(float value)
        {
           return Math.Round(Convert.ToDouble(value),1);
        }
        /// <summary>
        /// 返回区域的中间位置
        ///统一文字宽度：25pix
        ///实管：125pix
        ///桥式滤水管：220pix
        ///粘土封闭：190pix
        ///砾料：128pix
        ///粘土球：210pix
        /// </summary>
        /// <param name="rectangleF"></param>
        /// <returns></returns>
        public RectangleF GetRectangleF(RectangleF rectangleF,string Material)
        {
            
            switch (Material)
            {
                case "实管":
                    if (rectangleF.Height-125>0)
                    {
                        return new RectangleF(rectangleF.X+30, rectangleF.Y + (rectangleF.Height - 125) / 2, 25, 155);
                    }
                    else
                    {
                        return rectangleF;
                    }
                    
                case "桥式滤水管":
                    if (rectangleF.Height-220>0)
                    {
                        return new RectangleF(rectangleF.X + 30, rectangleF.Y + (rectangleF.Height - 220) / 2, 25, 220);
                    }
                    else
                    {
                        return rectangleF;
                    }
                    
                case "粘土封闭":
                    if (rectangleF.Height-190>0)
                    {
                        return new RectangleF(rectangleF.X + 30, rectangleF.Y + (rectangleF.Height - 190) / 2, 25, 190);
                    }
                    else
                    {
                        return rectangleF;
                    }
                    
                case "砾料":
                    if (rectangleF.Height-128>0)
                    {
                        return new RectangleF(rectangleF.X + 30, rectangleF.Y + (rectangleF.Height - 128) / 2, 25, 158);
                    }
                    else
                    {
                        return rectangleF;
                    }
                    
                case "粘土球":
                    if (rectangleF.Height-210>0)
                    {
                        return new RectangleF(rectangleF.X + 30, rectangleF.Y + (rectangleF.Height - 210) / 2, 25, 210);
                    }
                    else
                    {
                        return rectangleF;
                    }
                    
                default:
                    return rectangleF;
                    
            }
            
        }
    }   
    

}