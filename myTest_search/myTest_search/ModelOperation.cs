using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// 数据库命名空间
//using MySQLDriverCS;
using MySql.Data.MySqlClient;  
// halcon命名空间
using HalconDotNet;

namespace myTest_search
{
    public partial class ModelOperation : Form
    {
        HDevelopExportModel HDSon = new HDevelopExportModel();
        HDevelopExport HD = new HDevelopExport();
        public HTuple[] modelHandle = new HTuple[5];
        public  HTuple[] LCM00 = new HTuple[4];
        public  HTuple[] LCM01 = new HTuple[4];
        public  HTuple[] LCM02 = new HTuple[4];
        public ModelOperation()
        {
            InitializeComponent();
        }

        private void ModelOperation_Load(object sender, EventArgs e)
        {
            // 窗口载入事件，待实现
        }

        private void CreatModel(object sender, EventArgs e)
        {
            // 建立模板按钮
            string ImagePath;       // 定义模板图片的路径
            HObject readImage = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "BMP文件|*.bmp*|PNG文件|*.png*|JPEG文件|*.jpg*";     //模板文件格式
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 如果可以打开该文件路径，将该文件路径的图片显示在hWindowControl2窗口，并将图片变量赋值给readImage
                ImagePath = openFileDialog1.FileName;
                readImage = HDSon.ReadPicture(hWindowControl2.HalconWindow, ImagePath);
            }
            // 根据读到的图片创建模板
            HDSon.CreateModelPicture(hWindowControl2.HalconWindow, readImage);
        }

        private void DeleateModel(object sender, EventArgs e)
        {
            // 删除模板, 将模板区域置空
            HDSon.ho_KeyTemplateImage = null;
            HDSon.ho_ParallelCTemplateImage = null;
            HDSon.ho_VerticalCTemplateImage = null;
            HDSon.ho_LTemplateImage = null;
            HDSon.ho_JKTemplateImage = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 模板完成按钮，退出该窗口，并进行确认
            if (MessageBox.Show("模板操作已完成？", "退出模板操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void CreatCameraModelbutton00_Click(object sender, EventArgs e)
        {
            string ImagePath00, ImagePath01, ImagePath02;       // 定义模板图片的路径
            HObject readImage00 = null, readImage01 = null, readImage02 = null;
            ImagePath00 = "C:/Users/Administrator/Desktop/getPicture/littleCameraModle/00.bmp";
            ImagePath01 = "C:/Users/Administrator/Desktop/getPicture/littleCameraModle/01.bmp";
            ImagePath02 = "C:/Users/Administrator/Desktop/getPicture/littleCameraModle/02.bmp";
            readImage00 = HDSon.ReadPicture(hWindowControl2.HalconWindow, ImagePath00);
            HDSon.disp_message(hWindowControl2.HalconWindow, "在二极管处画一矩形区域1", "window", 12, 12, "forest green", "true");
            LCM00 = HDSon.CreateLittleCameraModel00(hWindowControl2.HalconWindow, readImage00);
            readImage01 = HDSon.ReadPicture(hWindowControl2.HalconWindow, ImagePath01);
            HDSon.disp_message(hWindowControl2.HalconWindow, "在二极管处画一矩形区域2", "window", 12, 12, "forest green", "true");
            LCM01 = HDSon.CreateLittleCameraModel01(hWindowControl2.HalconWindow, readImage01);
            readImage02 = HDSon.ReadPicture(hWindowControl2.HalconWindow, ImagePath02);
            HDSon.disp_message(hWindowControl2.HalconWindow, "在二极管处画一矩形区域3", "window", 12, 12, "forest green", "true");
            LCM02 = HDSon.CreateLittleCameraModel02(hWindowControl2.HalconWindow, readImage02);

            
            //this.dataGridView1.DataSource = db.getPsize().Tables["psize"];
          //  db.closeConn();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 删除小相机模板button
            HDSon.deleteLittleCameraModel();
        }
             
    }


    public partial class HDevelopExportModel
    {

        public HTuple[] modelHandle = new HTuple[5];

        // Local iconic variables 
        public HObject ho_KeyTemplateImage;
        public HObject ho_VerticalCTemplateImage;
        public HObject ho_ParallelCTemplateImage;
        public HObject ho_LTemplateImage;
        public HObject ho_JKTemplateImage;


        HObject ho_Image;
        HObject ho_ImageModel, ho_KeyModelRegion;
        HObject ho_VetrticalCModelRegion;
        HObject ho_ParallelCModelRegion;
        HObject ho_LModelRegion, ho_JKModelRegion;
        HObject ho_KeyModelContours, ho_KeyTransContours = null;
        HObject ho_VerticalCModelContours, ho_VerticalCTransContours = null;
        HObject ho_ParallelCModelContours, ho_ParallelCTransContours = null;
        HObject ho_LModelContours, ho_LTransContours = null, ho_JKModelContours;
        HObject ho_JKTransContours = null, ho_KeyCross = null, ho_VerticalCCross = null;
        HObject ho_ParallelCCross = null, ho_LCross = null, ho_JKCross = null;
        // add at 17/02/23 13:12
        HObject ho_RectangleVerticalC, ho_RectangleParallelC, ho_ImageVCReduced;
        HObject ho_ImagePCRedeced, ho_ImageEmphasize, ho_ImagePCEmphasize;
        HObject ho_ReviewRegion, ho_ReviewPCRegion, ho_ReviewRegionFillUp;
        HObject ho_ReviewPCRegionFillUP, ho_ReviewConnectedRegions;
        HObject ho_ReviewPCConnectedRegions, ho_ReviewSelectedRegions;
        HObject ho_ReviewPCSelectedRegions, ho_RegionDilation, ho_RegionPCDilation;
        HObject ho_RegionUnion, ho_RegionPCUnion, ho_ContoursVC;
        HObject ho_ContoursPC;
        // add at 17/02/24 14:18
        HObject ho_Image00, ho_Image01, ho_Image02;
        HObject ho_Rectangle00, ho_Rectangle01, ho_Rectangle02;
        HObject ho_ImageReduced00, ho_ImageReduced01, ho_ImageReduced02;
        HObject ho_ImagePart00, ho_ImagePart01, ho_ImagePart02;
        HObject ho_GrayImage00, ho_GrayImage01, ho_GrayImage02;
        HObject ho_ImageEmphasize00, ho_ImageEmphasize01, ho_ImageEmphasize02;
        HObject ho_Region00, ho_Region01, ho_Region02, ho_ConnectedRegions00;
        HObject ho_ConnectedRegions01, ho_ConnectedRegions02, ho_SelectedRegionsLittle00;
        HObject ho_SelectedRegionsBig00, ho_SelectedRegionsLittle01;
        HObject ho_SelectedRegionsBig01, ho_SelectedRegionsLittle02;
        HObject ho_SelectedRegionsBig02;
        // add at 17/02/25 08:40
        HObject ho_ContoursLittle00, ho_ContoursBig00;
        HObject ho_ContoursLittle01, ho_ContoursBig01;
        HObject ho_ContoursLittle02, ho_ContoursBig02;

        // Local control variables 
        HTuple partNum; //主面板零件个数
        HTuple hv_Width, hv_Height, hv_resultNum, hv_Duration = null;
        HTuple hv_RowK1 = null, hv_Start = null, hv_Stop = null;
        HTuple hv_ColumnK1 = null, hv_RowK2 = null, hv_ColumnK2 = null;
        HTuple hv_KeyModelId = null, hv_RowVC1 = null, hv_ColumnVC1 = null;
        HTuple hv_RowVC2 = null, hv_ColumnVC2 = null, hv_VerticalCModelId = null;
        HTuple hv_RowPC1 = null, hv_ColumnPC1 = null, hv_RowPC2 = null;
        HTuple hv_ColumnPC2 = null, hv_ParallelCModelId = null;
        HTuple hv_RowL1 = null, hv_ColumnL1 = null, hv_RowL2 = null;
        HTuple hv_ColumnL2 = null, hv_LModelId = null, hv_RowJK1 = null;
        HTuple hv_ColumnJK1 = null, hv_RowJK2 = null, hv_ColumnJK2 = null;
        HTuple hv_JKModelId = null, hv_KeyModelRow = null;
        HTuple hv_KeyModelColumn = null, hv_KeyModelAngle = null;
        HTuple hv_KeyModelScore = null, hv_MatchingObjIdx = null;
        HTuple hv_HomMat = new HTuple(), hv_VerticalCModelRow = null;
        HTuple hv_VerticalCModelColumn = null, hv_VerticalCModelAngle = null;
        HTuple hv_VerticalCModelScore = null, hv_ParallelCModelRow = null;
        HTuple hv_ParallelCModelColumn = null, hv_ParallelCModelAngle = null;
        HTuple hv_ParallelCModelScore = null, hv_LModelRow = null;
        HTuple hv_LModelColumn = null, hv_LModelAngle = null, hv_LModelScore = null;
        HTuple hv_JKModelRow = null, hv_JKModelColumn = null, hv_JKModelAngle = null;
        HTuple hv_JKModelScore = null, hv_Index = null, hv_Index1 = null;
        HTuple hv_Index2 = null, hv_Index3 = null, hv_Index4 = null;
        // add at 17/02/23 13:12
        HTuple hv_VCPartWidth = null, hv_VCPartHeight = null, hv_PCPartWith = null;
        HTuple hv_PCPartHeight = null, hv_VCPartArea = null, hv_VCPartRow = null;
        HTuple hv_VCpartColumn = null, hv_PCPartArea = null, hv_PCPartRow = null;
        HTuple hv_PCPartColumn = null, hv_ReviewArea = null, hv_ReviewRow = null;
        HTuple hv_ReviewColumn = null, hv_ReviewPCArea = null;
        HTuple hv_ReviewPCRow = null, hv_ReviewPCColumn = null;
        HTuple hv_ReviewVC = new HTuple(), hv_ReviewPC = new HTuple();
        // add at 17/02/24 14:18
       
        HTuple hv_Width00 = null, hv_Height00 = null;
        HTuple hv_WindowHandle00 = null, hv_WindowHandle01 = null;
        HTuple hv_WindowHandle02 = null;
        HTuple hv_Width01 = null, hv_Height01 = null;
        HTuple hv_Width02 = null, hv_Height02 = null, hv_Area00Little = null;
        HTuple hv_Row00Little = null, hv_Column00Little = null;
        HTuple hv_Area00Big = null, hv_Row00Big = null, hv_Column00Big = null;
        HTuple hv_Area01Little = null, hv_Row01Little = null, hv_Column01Little = null;
        HTuple hv_Area01Big = null, hv_Row01Big = null, hv_Column01Big = null;
        HTuple hv_Area02Little = null, hv_Row02Little = null, hv_Column02Little = null;
        HTuple hv_Area02Big = null, hv_Row02Big = null, hv_Column02Big = null;
        HTuple hv_Result00 = new HTuple(), hv_Result01 = new HTuple();
        HTuple hv_Result02 = new HTuple(), hv_AcqHandle = new HTuple();
        // 小相机的最后检测结果输出,0为与后结果，1,2,3分别为三个小相机的结果
        HTuple []hv_AllLittleCamera = new HTuple[4];
        // changed at  17/02/24 16:30
        public HTuple hv_Row00LU;
        public HTuple hv_Column00LU;
        public HTuple hv_Row00RD;
        public HTuple hv_Column00RD;
        public HTuple hv_Row01LU;
        public HTuple hv_Column01LU;
        public HTuple hv_Row01RD;
        public HTuple hv_Column01RD;
        public HTuple hv_Row02LU;
        public HTuple hv_Column02LU;
        public HTuple hv_Row02RD;
        public HTuple hv_Column02RD;

        // Procedures 
        // External procedures 
        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image. 

        // 窗口句柄

        public HTuple hv_ExpDefaultWinHandleModel;
        public HTuple hv_ExpDefaultWinHandleModel00;
        public HTuple hv_ExpDefaultWinHandleModel01;
        public HTuple hv_ExpDefaultWinHandleModel02;

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message.

        public void SaveMySql(int id, double hv_RowLU, double hv_ColumnLU, double hv_RowRD, double hv_ColumnRD)
        {
            /*MySQLConnection conn = new MySQLConnection(new MySQLConnectionString("localhost", "mysql", "root", "12345").AsString);
            //打开数据库
            conn.Open();
            //防止乱码
            MySQLCommand commn = new MySQLCommand("set names gbk", conn);
            commn.ExecuteNonQuery();
            //string sql = "select id,user_name,password from user_inf"; //命令语句
            string sql = "insert into littlecameramodel values(id, hv_RowLU, hv_ColumnLU, hv_RowRD, hv_ColumnRD)";
            MySQLCommand mycmd1 = new MySQLCommand(sql, conn);
            mycmd1.ExecuteNonQuery();
            conn.Close();
            //DataTable dt = new DataTable();
            // mda.Fill(dt);*/
            string mysqlConnectionString = "Server=localhost;Database=mysql;Uid=root;Pwd=12345";
            MySqlConnection conn = new MySqlConnection(mysqlConnectionString);
            MySqlCommand command;
            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = "INSERT INTO test2(id,R1,C1,R2,C2) VALUES(@id,@R1,@C1,@R2,@C2)";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@R1", hv_RowLU);
            command.Parameters.AddWithValue("@C1", hv_ColumnLU);
            command.Parameters.AddWithValue("@R2", hv_RowRD);
            command.Parameters.AddWithValue("@C2", hv_ColumnRD);
            command.ExecuteNonQuery();
            conn.Close();  


        }

        public HObject ReadPicture(HTuple window, string ImagePath)
        {
            // 得到图片显示的窗口句柄
            hv_ExpDefaultWinHandleModel = window;
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImagePath);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetWindowAttr("background_color", "black");
            //调整窗口显示大小以适应图片
            HOperatorSet.SetPart(hv_ExpDefaultWinHandleModel, 0, 0, hv_Height - 1, hv_Width - 1);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
            return ho_Image;
        }

        public void deleteLittleCameraModel()
        {
            // 删除小相机模板，所有坐标变量置空
            hv_Row00LU = null;
            hv_Column00LU = null;
            hv_Row00RD = null;
            hv_Column00RD = null;
            hv_Row01LU = null;
            hv_Column01LU = null;
            hv_Row01RD = null;
            hv_Column01RD = null;
            hv_Row02LU = null;
            hv_Column02LU = null;
            hv_Row02RD = null;
            hv_Column02RD = null;
        }

        public HTuple[] CreateModelPicture(HTuple window,HObject ho_ImageRead)
        {
            // 该函数用来创建模板，模板区域为（ho_***TemplateImage），并将已创建好的模板的ID返回
            // Initialize local and output iconic variables 
            hv_ExpDefaultWinHandleModel = window;
            ho_Image = ho_ImageRead;
            HOperatorSet.GenEmptyObj(out ho_KeyModelRegion);
            HOperatorSet.GenEmptyObj(out ho_KeyTemplateImage);
            HOperatorSet.GenEmptyObj(out ho_VetrticalCModelRegion);
            HOperatorSet.GenEmptyObj(out ho_VerticalCTemplateImage);
            HOperatorSet.GenEmptyObj(out ho_ParallelCModelRegion);
            HOperatorSet.GenEmptyObj(out ho_ParallelCTemplateImage);
            HOperatorSet.GenEmptyObj(out ho_LModelRegion);
            HOperatorSet.GenEmptyObj(out ho_LTemplateImage);
            HOperatorSet.GenEmptyObj(out ho_JKModelRegion);
            HOperatorSet.GenEmptyObj(out ho_JKTemplateImage);
           
            // 开始读取图像并建立模板
            // 开始创建key键值模板
            disp_message(hv_ExpDefaultWinHandleModel, "请在任一按键处画一矩形区域,完成后单击右键","window", 12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandleModel, out hv_RowK1, out hv_ColumnK1, out hv_RowK2, out hv_ColumnK2);
            ho_KeyModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_KeyModelRegion, hv_RowK1, hv_ColumnK1, hv_RowK2, hv_ColumnK2);
            ho_KeyTemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_KeyModelRegion,  out ho_KeyTemplateImage);
            //Matching 02: create the shape model
            HOperatorSet.CreateShapeModel(ho_KeyTemplateImage, 5, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), (new HTuple(0.8551)).TupleRad(), (new HTuple("none")).TupleConcat(
                "no_pregeneration"), "use_polarity", ((new HTuple(51)).TupleConcat(64)).TupleConcat(4), 19, out hv_KeyModelId);
            modelHandle[0] = hv_KeyModelId;
            // key键值模板创建完毕，开始建立竖直电容模板
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
            disp_message(hv_ExpDefaultWinHandleModel, "请在竖直电容处画一矩形区域,完成后单击右键", "window", 12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandleModel, out hv_RowVC1, out hv_ColumnVC1,
                out hv_RowVC2, out hv_ColumnVC2);
            ho_VetrticalCModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_VetrticalCModelRegion, hv_RowVC1, hv_ColumnVC1,
                hv_RowVC2, hv_ColumnVC2);
            //Matching 03: reduce the model template
            ho_VerticalCTemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_VetrticalCModelRegion, out ho_VerticalCTemplateImage
                );
            //Matching 03: create the shape model
            HOperatorSet.CreateShapeModel(ho_VerticalCTemplateImage, 5, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), (new HTuple(0.7103)).TupleRad(), (new HTuple("none")).TupleConcat(
                "no_pregeneration"), "use_polarity", ((new HTuple(35)).TupleConcat(46)).TupleConcat(4), 24, out hv_VerticalCModelId);
            modelHandle[1] = hv_VerticalCModelId;
            // 竖直电容模板创建完毕，建立水平电容模板
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
            disp_message(hv_ExpDefaultWinHandleModel, "请在横向电容处画一矩形区域,完成后单击右键", "window", 12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandleModel, out hv_RowPC1, out hv_ColumnPC1, out hv_RowPC2, out hv_ColumnPC2);
            ho_ParallelCModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_ParallelCModelRegion, hv_RowPC1, hv_ColumnPC1, hv_RowPC2, hv_ColumnPC2);
            //Matching 04: reduce the model template
            ho_ParallelCTemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_ParallelCModelRegion, out ho_ParallelCTemplateImage
                );
            //Matching 04: create the shape model
            HOperatorSet.CreateShapeModel(ho_ParallelCTemplateImage, 4, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), (new HTuple(0.5288)).TupleRad(), (new HTuple("point_reduction_high")).TupleConcat(
                "no_pregeneration"), "use_polarity", ((new HTuple(24)).TupleConcat(42)).TupleConcat(
                6), 10, out hv_ParallelCModelId);
            modelHandle[2] = hv_ParallelCModelId;
            // 建立竖直电感模板
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
            disp_message(hv_ExpDefaultWinHandleModel, "请在竖向电感处画一矩形区域,完成后单击右键",
                    "window", 12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandleModel, out hv_RowL1, out hv_ColumnL1, out hv_RowL2,
                out hv_ColumnL2);
            ho_LModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_LModelRegion, hv_RowL1, hv_ColumnL1, hv_RowL2,
                hv_ColumnL2);
            //Matching 05: reduce the model template
            ho_LTemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_LModelRegion, out ho_LTemplateImage);
            //Matching 05: create the shape model
            HOperatorSet.CreateShapeModel(ho_LTemplateImage, 4, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), (new HTuple(0.5904)).TupleRad(), (new HTuple("point_reduction_high")).TupleConcat(
                "no_pregeneration"), "use_polarity", ((new HTuple(22)).TupleConcat(36)).TupleConcat(
                4), 10, out hv_LModelId);
            modelHandle[3] = hv_LModelId;
            // 建立JK模板
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
            disp_message(hv_ExpDefaultWinHandleModel, "请在JK处画一矩形区域,完成后单击右键", "window",
                    12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandleModel, out hv_RowJK1, out hv_ColumnJK1,
                out hv_RowJK2, out hv_ColumnJK2);
            ho_JKModelRegion.Dispose();
            HOperatorSet.GenRectangle1(out ho_JKModelRegion, hv_RowJK1, hv_ColumnJK1, hv_RowJK2,
                hv_ColumnJK2);
            //Matching 06: reduce the model template
            ho_JKTemplateImage.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_JKModelRegion, out ho_JKTemplateImage
                );
            //Matching 06: create the shape model
            HOperatorSet.CreateShapeModel(ho_JKTemplateImage, 6, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), (new HTuple(0.3575)).TupleRad(), (new HTuple("point_reduction_high")).TupleConcat(
                "no_pregeneration"), "use_polarity", ((new HTuple(38)).TupleConcat(49)).TupleConcat(
                4), 27, out hv_JKModelId);
            modelHandle[4] = hv_LModelId;
            // 将各个模板的id返回
            ho_Image.Dispose();
            ho_KeyModelRegion.Dispose();
            ho_KeyTemplateImage.Dispose();
            ho_VetrticalCModelRegion.Dispose();
            ho_VerticalCTemplateImage.Dispose();
            ho_ParallelCModelRegion.Dispose();
            ho_ParallelCTemplateImage.Dispose();
            ho_LModelRegion.Dispose();
            ho_LTemplateImage.Dispose();
            ho_JKModelRegion.Dispose();
            ho_JKTemplateImage.Dispose(); 

            return modelHandle;
        }
        // 创建小相机模板ROI
        public HTuple[] CreateLittleCameraModel00(HTuple Windows, HObject ho_ImageModel00)
        {
            HTuple []LCM00 = new HTuple [4];
            HOperatorSet.DrawRectangle1(Windows, out hv_Row00LU, out hv_Column00LU, out hv_Row00RD, out hv_Column00RD);
            LCM00[0] = hv_Row00LU; LCM00[1] = hv_Column00LU;
            LCM00[2] = hv_Row00RD; LCM00[3] = hv_Column00RD;
            return LCM00;
        }
        public HTuple[] CreateLittleCameraModel01(HTuple Windows, HObject ho_ImageModel01)
        {
            HTuple[] LCM01 = new HTuple[4];
            HOperatorSet.DrawRectangle1(Windows, out hv_Row01LU, out hv_Column01LU, out hv_Row01RD, out hv_Column01RD);
            LCM01[0] = hv_Row01LU; LCM01[1] = hv_Column01LU;
            LCM01[2] = hv_Row01RD; LCM01[3] = hv_Column01RD;
            return LCM01;
        }
        public HTuple[] CreateLittleCameraModel02(HTuple Windows, HObject ho_ImageModel02)
        {
            HTuple[] LCM02 = new HTuple[4];
            HOperatorSet.DrawRectangle1(Windows, out hv_Row02LU, out hv_Column02LU, out hv_Row02RD, out hv_Column02RD);
            LCM02[0] = hv_Row02LU; LCM02[1] = hv_Column02LU;
            LCM02[2] = hv_Row02RD; LCM02[3] = hv_Column02RD;
            return LCM02;
        }


        public HTuple []ProcessLittleCamera(HTuple Windows00, HTuple Windows01, HTuple Windows02)
        {
            // 根据之前画的模板对二极管的极性进行处理，
            // 提取出区域后通过比较横坐标的大小来判断二极管是否放反
            // add at 17/02/25 8:40
            ModelOperation myData = new ModelOperation();
            hv_Row00LU = myData.LCM00[0];         
            
            HOperatorSet.GenEmptyObj(out ho_Rectangle00);
            HOperatorSet.GenEmptyObj(out ho_Rectangle01);
            HOperatorSet.GenEmptyObj(out ho_Rectangle02);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced00);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced01);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced02);
            HOperatorSet.GenEmptyObj(out ho_ImagePart00);
            HOperatorSet.GenEmptyObj(out ho_ImagePart01);
            HOperatorSet.GenEmptyObj(out ho_ImagePart02);
            HOperatorSet.GenEmptyObj(out ho_GrayImage00);
            HOperatorSet.GenEmptyObj(out ho_GrayImage01);
            HOperatorSet.GenEmptyObj(out ho_GrayImage02);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize00);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize01);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize02);
            HOperatorSet.GenEmptyObj(out ho_Region00);
            HOperatorSet.GenEmptyObj(out ho_Region01);
            HOperatorSet.GenEmptyObj(out ho_Region02);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions00);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions01);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions02);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsLittle00);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsBig00);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsLittle01);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsBig01);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsLittle02);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsBig02);
            HOperatorSet.GenEmptyObj(out ho_ContoursLittle00);
            HOperatorSet.GenEmptyObj(out ho_ContoursBig00);
            HOperatorSet.GenEmptyObj(out ho_ContoursLittle01);
            HOperatorSet.GenEmptyObj(out ho_ContoursBig01);
            HOperatorSet.GenEmptyObj(out ho_ContoursLittle02);
            HOperatorSet.GenEmptyObj(out ho_ContoursBig02);
            ho_Rectangle00.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle00, hv_Row00LU, hv_Column00LU, hv_Row00RD,
            hv_Column00RD);
            ho_Rectangle01.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle01, hv_Row01LU, hv_Column01LU, hv_Row01RD,
                hv_Column01RD);
            ho_Rectangle02.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle02, hv_Row02LU, hv_Column02LU, hv_Row02RD,
                hv_Column02RD);
            ho_ImageReduced00.Dispose();
            // 修改图像的定义域
            HOperatorSet.ReduceDomain(ho_Image00, ho_Rectangle00, out ho_ImageReduced00);
            ho_ImageReduced01.Dispose();
            HOperatorSet.ReduceDomain(ho_Image01, ho_Rectangle01, out ho_ImageReduced01);
            ho_ImageReduced02.Dispose();
            HOperatorSet.ReduceDomain(ho_Image02, ho_Rectangle02, out ho_ImageReduced02);
            ho_ImagePart00.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced00, out ho_ImagePart00);
            ho_ImagePart01.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced01, out ho_ImagePart01);
            ho_ImagePart02.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced02, out ho_ImagePart02);
            // 转成灰度图
            ho_GrayImage00.Dispose();
            HOperatorSet.Rgb1ToGray(ho_ImagePart00, out ho_GrayImage00);
            ho_GrayImage01.Dispose();
            HOperatorSet.Rgb1ToGray(ho_ImagePart01, out ho_GrayImage01);
            ho_GrayImage02.Dispose();
            HOperatorSet.Rgb1ToGray(ho_ImagePart02, out ho_GrayImage02);
            HOperatorSet.GetImageSize(ho_GrayImage00, out hv_Width00, out hv_Height00);
            HOperatorSet.GetImageSize(ho_GrayImage01, out hv_Width01, out hv_Height01);
            HOperatorSet.GetImageSize(ho_GrayImage02, out hv_Width02, out hv_Height02);
            // 增大对比度
            ho_ImageEmphasize00.Dispose();
            HOperatorSet.Emphasize(ho_GrayImage00, out ho_ImageEmphasize00, hv_Width00, hv_Height00,
                2);
            ho_ImageEmphasize01.Dispose();
            HOperatorSet.Emphasize(ho_GrayImage01, out ho_ImageEmphasize01, hv_Width01, hv_Height01,
                2);
            ho_ImageEmphasize02.Dispose();
            HOperatorSet.Emphasize(ho_GrayImage02, out ho_ImageEmphasize02, hv_Width02, hv_Height01,
                2);
            // 根据灰度值对图像进行提取，根据面积进行提取
            ho_Region00.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize00, out ho_Region00, 0, 80);
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize01, out ho_Region01, 0, 80);
            ho_Region02.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize02, out ho_Region02, 0, 80);
            ho_ConnectedRegions00.Dispose();
            HOperatorSet.Connection(ho_Region00, out ho_ConnectedRegions00);
            ho_ConnectedRegions01.Dispose();
            HOperatorSet.Connection(ho_Region01, out ho_ConnectedRegions01);
            ho_ConnectedRegions02.Dispose();
            HOperatorSet.Connection(ho_Region02, out ho_ConnectedRegions02);
            ho_SelectedRegionsLittle00.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions00, out ho_SelectedRegionsLittle00,
                "area", "and", 500, 1500);
            ho_SelectedRegionsBig00.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions00, out ho_SelectedRegionsBig00,
                "area", "and", 1500, 99999);
            ho_SelectedRegionsLittle01.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions01, out ho_SelectedRegionsLittle01,
                "area", "and", 500, 1500);
            ho_SelectedRegionsBig01.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions01, out ho_SelectedRegionsBig01,
                "area", "and", 1500, 99999);
            ho_SelectedRegionsLittle02.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegionsLittle02,
                "area", "and", 500, 1500);
            ho_SelectedRegionsBig02.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegionsBig02,
                "area", "and", 1500, 99999);
            //得到二极管的横坐标，用来比较其左右位置是否放置反
            HOperatorSet.AreaCenter(ho_SelectedRegionsLittle00, out hv_Area00Little, out hv_Row00Little,
                out hv_Column00Little);
            HOperatorSet.AreaCenter(ho_SelectedRegionsBig00, out hv_Area00Big, out hv_Row00Big,
                out hv_Column00Big);

            HOperatorSet.AreaCenter(ho_SelectedRegionsLittle01, out hv_Area01Little, out hv_Row01Little,
                out hv_Column01Little);
            HOperatorSet.AreaCenter(ho_SelectedRegionsBig01, out hv_Area01Big, out hv_Row01Big,
                out hv_Column01Big);

            HOperatorSet.AreaCenter(ho_SelectedRegionsLittle02, out hv_Area02Little, out hv_Row02Little,
                out hv_Column02Little);
            HOperatorSet.AreaCenter(ho_SelectedRegionsBig02, out hv_Area02Big, out hv_Row02Big,
                out hv_Column02Big);

            // 提取出区域轮廓，并将区域轮廓在窗口进行显示
            // 第一个小相机
            ho_ContoursLittle00.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsLittle00, out ho_ContoursLittle00,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsLittle00, hv_ExpDefaultWinHandleModel00);
            ho_ContoursBig00.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsBig00, out ho_ContoursBig00,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsBig00, hv_ExpDefaultWinHandleModel00);
            // 第二个小相机
            ho_ContoursLittle01.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsLittle01, out ho_ContoursLittle01,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsLittle01, hv_ExpDefaultWinHandleModel01);
            ho_ContoursBig01.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsBig01, out ho_ContoursBig01,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsBig01, hv_ExpDefaultWinHandleModel01);
            // 第三个小相机
            ho_ContoursLittle01.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsLittle02, out ho_ContoursLittle02,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsLittle02, hv_ExpDefaultWinHandleModel02);
            ho_ContoursBig01.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SelectedRegionsBig02, out ho_ContoursBig02,
                "border");
            HOperatorSet.DispObj(ho_SelectedRegionsLittle02, hv_ExpDefaultWinHandleModel02);
            // 最后判断
            if ((int)(new HTuple(hv_Column00Big.TupleLess(hv_Column00Little))) != 0)
            {
                hv_Result00 = "OK";
            }
            else
            {
                hv_Result00 = "NG";
            }
            hv_AllLittleCamera[1] = hv_Result00;
            if ((int)(new HTuple(hv_Column01Big.TupleLess(hv_Column01Little))) != 0)
            {
                hv_Result01 = "OK";
            }
            else
            {
                hv_Result01 = "NG";
            }
            hv_AllLittleCamera[2] = hv_Result01;
            if ((int)(new HTuple(hv_Column02Big.TupleLess(hv_Column02Little))) != 0)
            {
                hv_Result02 = "OK";
            }
            else
            {
                hv_Result02 = "NG";
            }
            hv_AllLittleCamera[3] = hv_Result02;
            if ((hv_Result00 == "OK") && (hv_Result01 == "OK") && (hv_Result02 == "OK"))
            {
                hv_AllLittleCamera[0] = "OK";
            }
            else
            {
                hv_AllLittleCamera[0] = "NG";
            }

            return hv_AllLittleCamera;
        }



        public HObject GetImageFromCamera(HTuple window, HTuple hv_AcqHandle)
        {
            hv_ExpDefaultWinHandleModel = window;
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            return ho_Image;
        }

        public HTuple ModelMatching(HTuple[] modelHandle, HTuple window, int hv_cameraSignalFlag, HTuple hv_AcqHandle, HObject gotImage)
        {
            // 将窗口实参赋值进窗口句柄
            hv_ExpDefaultWinHandleModel = window;
            HOperatorSet.SetColor(hv_ExpDefaultWinHandleModel, "green");
            hv_resultNum = null;      //最终检测结果，以NG和OK表示
            partNum = 0;
            // init 
            HOperatorSet.GenEmptyObj(out ho_KeyModelContours);
            HOperatorSet.GenEmptyObj(out ho_KeyTransContours);
            HOperatorSet.GenEmptyObj(out ho_VerticalCModelContours);
            HOperatorSet.GenEmptyObj(out ho_VerticalCTransContours);
            HOperatorSet.GenEmptyObj(out ho_ParallelCModelContours);
            HOperatorSet.GenEmptyObj(out ho_ParallelCTransContours);
            HOperatorSet.GenEmptyObj(out ho_LModelContours);
            HOperatorSet.GenEmptyObj(out ho_LTransContours);
            HOperatorSet.GenEmptyObj(out ho_JKModelContours);
            HOperatorSet.GenEmptyObj(out ho_JKTransContours);
            HOperatorSet.GenEmptyObj(out ho_KeyCross);
            HOperatorSet.GenEmptyObj(out ho_VerticalCCross);
            HOperatorSet.GenEmptyObj(out ho_ParallelCCross);
            HOperatorSet.GenEmptyObj(out ho_LCross);
            HOperatorSet.GenEmptyObj(out ho_JKCross);
            HOperatorSet.GenEmptyObj(out ho_ImageModel);

            while (hv_cameraSignalFlag == 1)
            {
                // 获得一个新的信号源之后，将窗口进行清除
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
                // 获取图像 ho_Image, 并计算运行时间1
                HOperatorSet.CountSeconds(out hv_Start);
                // 打开相机拍照后得到的图像赋值给ho_Image
                ho_Image = gotImage;
                // 根据相机句柄开始获取图像
               // HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                //Image Acquisition 01: Do something
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(hv_ExpDefaultWinHandleModel, 0, 0, hv_Height - 1, hv_Width - 1);
                // 将拿到的图像进行显示，并交互画出ROI
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
                
                // key 键匹配
                ho_KeyModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_KeyModelContours, modelHandle[0], 1);
                //Matching 02: END of generated code for model initialization
                //Matching 02: BEGIN of generated code for model application
                //Matching 02: the following operations are usually moved into
                //Matching 02: that loop where the aquired images are processed
                //Matching 02: Find the model
                HOperatorSet.FindShapeModel(ho_Image, modelHandle[0], (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.19, 19, 0.5, "least_squares", (new HTuple(5)).TupleConcat(
                    1), 0.75, out hv_KeyModelRow, out hv_KeyModelColumn, out hv_KeyModelAngle,
                    out hv_KeyModelScore);
                //Matching 02: transform the model contours into the detected positions
                for (hv_MatchingObjIdx = 0; (int)hv_MatchingObjIdx <= (int)((new HTuple(hv_KeyModelScore.TupleLength()
                    )) - 1); hv_MatchingObjIdx = (int)hv_MatchingObjIdx + 1)
                {
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat);
                    HOperatorSet.HomMat2dRotate(hv_HomMat, hv_KeyModelAngle.TupleSelect(hv_MatchingObjIdx),
                        0, 0, out hv_HomMat);
                    HOperatorSet.HomMat2dTranslate(hv_HomMat, hv_KeyModelRow.TupleSelect(hv_MatchingObjIdx),
                        hv_KeyModelColumn.TupleSelect(hv_MatchingObjIdx), out hv_HomMat);
                    ho_KeyTransContours.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_KeyModelContours, out ho_KeyTransContours,
                        hv_HomMat);
                    HOperatorSet.DispObj(ho_KeyTransContours, hv_ExpDefaultWinHandleModel);
                    partNum++;
                }
                // 竖向电容匹配
                ho_VerticalCModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_VerticalCModelContours, modelHandle[1], 1);
                //Matching 03: END of generated code for model initialization
                //Matching 03: BEGIN of generated code for model application
                //Matching 03: the following operations are usually moved into
                //Matching 03: that loop where the aquired images are processed
                //Matching 03: Find the model
                HOperatorSet.FindShapeModel(ho_Image, modelHandle[1], (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.2, 12, 0.5, "least_squares", (new HTuple(5)).TupleConcat(
                    1), 0.75, out hv_VerticalCModelRow, out hv_VerticalCModelColumn, out hv_VerticalCModelAngle,
                    out hv_VerticalCModelScore);
                //Matching 03: transform the model contours into the detected positions
                for (hv_MatchingObjIdx = 0; (int)hv_MatchingObjIdx <= (int)((new HTuple(hv_VerticalCModelScore.TupleLength()
                    )) - 1); hv_MatchingObjIdx = (int)hv_MatchingObjIdx + 1)
                {
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat);
                    HOperatorSet.HomMat2dRotate(hv_HomMat, hv_VerticalCModelAngle.TupleSelect(
                        hv_MatchingObjIdx), 0, 0, out hv_HomMat);
                    HOperatorSet.HomMat2dTranslate(hv_HomMat, hv_VerticalCModelRow.TupleSelect(
                        hv_MatchingObjIdx), hv_VerticalCModelColumn.TupleSelect(hv_MatchingObjIdx),
                        out hv_HomMat);
                    ho_VerticalCTransContours.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_VerticalCModelContours, out ho_VerticalCTransContours,
                        hv_HomMat);
                    HOperatorSet.DispObj(ho_VerticalCTransContours, hv_ExpDefaultWinHandleModel);
                    partNum++;
                }
                // 横向电容
                ho_ParallelCModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ParallelCModelContours, modelHandle[2], 1);
                //Matching 04: END of generated code for model initialization
                //Matching 04: BEGIN of generated code for model application
                //Matching 04: the following operations are usually moved into
                //Matching 04: that loop where the aquired images are processed
                //Matching 04: Find the model
                HOperatorSet.FindShapeModel(ho_Image, modelHandle[2], (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.29, 12, 0.5, "least_squares", (new HTuple(4)).TupleConcat(
                    1), 0.75, out hv_ParallelCModelRow, out hv_ParallelCModelColumn, out hv_ParallelCModelAngle,
                    out hv_ParallelCModelScore);
                //Matching 04: transform the model contours into the detected positions
                for (hv_MatchingObjIdx = 0; (int)hv_MatchingObjIdx <= (int)((new HTuple(hv_ParallelCModelScore.TupleLength()
                    )) - 1); hv_MatchingObjIdx = (int)hv_MatchingObjIdx + 1)
                {
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat);
                    HOperatorSet.HomMat2dRotate(hv_HomMat, hv_ParallelCModelAngle.TupleSelect(
                        hv_MatchingObjIdx), 0, 0, out hv_HomMat);
                    HOperatorSet.HomMat2dTranslate(hv_HomMat, hv_ParallelCModelRow.TupleSelect(
                        hv_MatchingObjIdx), hv_ParallelCModelColumn.TupleSelect(hv_MatchingObjIdx),
                        out hv_HomMat);
                    ho_ParallelCTransContours.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_ParallelCModelContours, out ho_ParallelCTransContours,
                        hv_HomMat);
                    HOperatorSet.DispObj(ho_ParallelCTransContours, hv_ExpDefaultWinHandleModel);
                    partNum++;
                }

                // 竖向电感
                ho_LModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_LModelContours, modelHandle[3], 1);
                //Matching 05: END of generated code for model initialization
                //Matching 05: BEGIN of generated code for model application
                //Matching 05: the following operations are usually moved into
                //Matching 05: that loop where the aquired images are processed
                //Matching 05: Find the model
                HOperatorSet.FindShapeModel(ho_Image, modelHandle[3], (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.18, 19, 0.5, "least_squares", (new HTuple(4)).TupleConcat(
                    1), 0.75, out hv_LModelRow, out hv_LModelColumn, out hv_LModelAngle, out hv_LModelScore);
                //Matching 05: transform the model contours into the detected positions
                for (hv_MatchingObjIdx = 0; (int)hv_MatchingObjIdx <= (int)((new HTuple(hv_LModelScore.TupleLength()
                    )) - 1); hv_MatchingObjIdx = (int)hv_MatchingObjIdx + 1)
                {
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat);
                    HOperatorSet.HomMat2dRotate(hv_HomMat, hv_LModelAngle.TupleSelect(hv_MatchingObjIdx),
                        0, 0, out hv_HomMat);
                    HOperatorSet.HomMat2dTranslate(hv_HomMat, hv_LModelRow.TupleSelect(hv_MatchingObjIdx),
                        hv_LModelColumn.TupleSelect(hv_MatchingObjIdx), out hv_HomMat);
                    ho_LTransContours.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_LModelContours, out ho_LTransContours,
                        hv_HomMat);
                    HOperatorSet.DispObj(ho_LTransContours, hv_ExpDefaultWinHandleModel);
                    partNum++;
                }

                // JK插孔
                ho_JKModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_JKModelContours, modelHandle[4], 1);
                //Matching 06: END of generated code for model initialization
                //Matching 06: BEGIN of generated code for model application
                //Matching 06: the following operations are usually moved into
                //Matching 06: that loop where the aquired images are processed
                //Matching 06: Find the model
                HOperatorSet.FindShapeModel(ho_Image, modelHandle[4], (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.3, 11, 0.5, "least_squares", (new HTuple(6)).TupleConcat(
                    1), 0.75, out hv_JKModelRow, out hv_JKModelColumn, out hv_JKModelAngle,
                    out hv_JKModelScore);
                //Matching 06: transform the model contours into the detected positions
                for (hv_MatchingObjIdx = 0; (int)hv_MatchingObjIdx <= (int)((new HTuple(hv_JKModelScore.TupleLength()
                    )) - 1); hv_MatchingObjIdx = (int)hv_MatchingObjIdx + 1)
                {
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat);
                    HOperatorSet.HomMat2dRotate(hv_HomMat, hv_JKModelAngle.TupleSelect(hv_MatchingObjIdx),
                        0, 0, out hv_HomMat);
                    HOperatorSet.HomMat2dTranslate(hv_HomMat, hv_JKModelRow.TupleSelect(hv_MatchingObjIdx),
                        hv_JKModelColumn.TupleSelect(hv_MatchingObjIdx), out hv_HomMat);
                    ho_JKTransContours.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_JKModelContours, out ho_JKTransContours,
                        hv_HomMat);
                    HOperatorSet.DispObj(ho_JKTransContours, hv_ExpDefaultWinHandleModel);
                    partNum++;
                }
                // 模板匹配结束，计算匹配的时间，并对模板位置进行输出
                HOperatorSet.CountSeconds(out hv_Stop);
                hv_Duration = (hv_Stop - hv_Start) * 1000;
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandleModel);
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandleModel);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandleModel, "green");
                // 在所匹配到的模板位置处画十字
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_KeyModelScore.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    ho_KeyCross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_KeyCross, hv_KeyModelRow.TupleSelect(
                        hv_Index), hv_KeyModelColumn.TupleSelect(hv_Index), 150, (new HTuple(90)).TupleRad()
                        );
                    HOperatorSet.DispObj(ho_KeyCross, hv_ExpDefaultWinHandleModel);
                }
                for (hv_Index1 = 0; (int)hv_Index1 <= (int)((new HTuple(hv_VerticalCModelScore.TupleLength()
                    )) - 1); hv_Index1 = (int)hv_Index1 + 1)
                {
                    ho_VerticalCCross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_VerticalCCross, hv_VerticalCModelRow.TupleSelect(
                        hv_Index1), hv_VerticalCModelColumn.TupleSelect(hv_Index1), 150, (new HTuple(90)).TupleRad()
                        );
                    HOperatorSet.DispObj(ho_VerticalCCross, hv_ExpDefaultWinHandleModel);
                }
                for (hv_Index2 = 0; (int)hv_Index2 <= (int)((new HTuple(hv_ParallelCModelScore.TupleLength()
                    )) - 1); hv_Index2 = (int)hv_Index2 + 1)
                {
                    ho_ParallelCCross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_ParallelCCross, hv_ParallelCModelRow.TupleSelect(
                        hv_Index2), hv_ParallelCModelColumn.TupleSelect(hv_Index2), 150, (new HTuple(90)).TupleRad()
                        );
                    HOperatorSet.DispObj(ho_ParallelCCross, hv_ExpDefaultWinHandleModel);
                }
                for (hv_Index3 = 0; (int)hv_Index3 <= (int)((new HTuple(hv_LModelScore.TupleLength()
                    )) - 1); hv_Index3 = (int)hv_Index3 + 1)
                {
                    ho_LCross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_LCross, hv_LModelRow.TupleSelect(hv_Index3),
                        hv_LModelColumn.TupleSelect(hv_Index3), 150, (new HTuple(90)).TupleRad()
                        );
                    HOperatorSet.DispObj(ho_LCross, hv_ExpDefaultWinHandleModel);
                }
                for (hv_Index4 = 0; (int)hv_Index4 <= (int)((new HTuple(hv_JKModelScore.TupleLength()
                    )) - 1); hv_Index4 = (int)hv_Index4 + 1)
                {
                    ho_JKCross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_JKCross, hv_JKModelRow.TupleSelect(
                        hv_Index4), hv_JKModelColumn.TupleSelect(hv_Index4), 150, (new HTuple(90)).TupleRad()
                        );
                    HOperatorSet.DispObj(ho_JKCross, hv_ExpDefaultWinHandleModel);
                }
                // 根据检测到的零件个数确定检测结果，并将模板匹配时间及检测时间在软件窗口显示
                if (partNum == 8)
                {
                    hv_resultNum = "OK";
                }
                else
                {
                    hv_resultNum = "NG";
                }
                disp_message(hv_ExpDefaultWinHandleModel, ((("检测时间：" + (hv_Duration.TupleString("3.0f"))) + " ms")).TupleConcat(
            "电路板缺件检测结果： " + hv_resultNum), "window", 12, 12, "forest green", "true");
                
                // 检测信号置0
                hv_cameraSignalFlag = 0;
            }
            ho_KeyModelContours.Dispose();
            ho_KeyTransContours.Dispose();
            ho_VerticalCModelContours.Dispose();
            ho_VerticalCTransContours.Dispose();
            ho_ParallelCModelContours.Dispose();
            ho_ParallelCTransContours.Dispose();
            ho_LModelContours.Dispose();
            ho_LTransContours.Dispose();
            ho_JKModelContours.Dispose();
            ho_JKTransContours.Dispose();
            ho_KeyCross.Dispose();
            ho_VerticalCCross.Dispose();
            ho_ParallelCCross.Dispose();
            ho_LCross.Dispose();
            ho_JKCross.Dispose();
            return hv_resultNum;
        }

        public HTuple CapacityReview(HTuple window)
        {
            // 检测横向和竖向电阻的极性，检测方式为：
            // 提取待检测零件的ROI，增大对比度，拿到ROI中极性特征偏白色区域，若该区域中心在零件ROI的左侧，则为OK，否则NG
            // add 
            HTuple ReviewResult = null;
            HOperatorSet.GenEmptyObj(out ho_RectangleVerticalC);
            HOperatorSet.GenEmptyObj(out ho_RectangleParallelC);
            HOperatorSet.GenEmptyObj(out ho_ImageVCReduced);
            HOperatorSet.GenEmptyObj(out ho_ImagePCRedeced);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImagePCEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ReviewRegion);
            HOperatorSet.GenEmptyObj(out ho_ReviewPCRegion);
            HOperatorSet.GenEmptyObj(out ho_ReviewRegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ReviewPCRegionFillUP);
            HOperatorSet.GenEmptyObj(out ho_ReviewConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ReviewPCConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ReviewSelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ReviewPCSelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionPCDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionPCUnion);
            HOperatorSet.GenEmptyObj(out ho_ContoursVC);
            HOperatorSet.GenEmptyObj(out ho_ContoursPC);

            ho_RectangleVerticalC.Dispose();
            // 异常时需要抛出，待实现
            HOperatorSet.GenRectangle1(out ho_RectangleVerticalC, (hv_VerticalCModelRow.TupleSelect(
                0)) - 86, (hv_VerticalCModelColumn.TupleSelect(0)) - 76, (hv_VerticalCModelRow.TupleSelect(
                0)) + 50, (hv_VerticalCModelColumn.TupleSelect(0)) + 92);
            ho_RectangleParallelC.Dispose();
            HOperatorSet.GenRectangle1(out ho_RectangleParallelC, hv_ParallelCModelRow - 75,
                hv_ParallelCModelColumn - 126, hv_ParallelCModelRow + 0, hv_ParallelCModelColumn + 166);
            ho_ImageVCReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_RectangleVerticalC, out ho_ImageVCReduced
                );
            ho_ImagePCRedeced.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_RectangleParallelC, out ho_ImagePCRedeced
                );
            HOperatorSet.GetImageSize(ho_ImageVCReduced, out hv_VCPartWidth, out hv_VCPartHeight);
            HOperatorSet.GetImageSize(ho_ImagePCRedeced, out hv_PCPartWith, out hv_PCPartHeight);
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_ImageVCReduced, out ho_ImageEmphasize, hv_VCPartWidth,
                hv_VCPartHeight, 1);
            ho_ImagePCEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_ImagePCRedeced, out ho_ImagePCEmphasize, hv_PCPartWith,
                hv_PCPartHeight, 1);
            HOperatorSet.AreaCenter(ho_ImageEmphasize, out hv_VCPartArea, out hv_VCPartRow,
                out hv_VCpartColumn);
            HOperatorSet.AreaCenter(ho_ImagePCEmphasize, out hv_PCPartArea, out hv_PCPartRow,
                out hv_PCPartColumn);
            //gen_rectangle2_contour_xld (RectangleVCXld, 70, 4, rad(0), 3, 35)
            ho_ReviewRegion.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize, out ho_ReviewRegion, 128, 180);
            ho_ReviewPCRegion.Dispose();
            HOperatorSet.Threshold(ho_ImagePCEmphasize, out ho_ReviewPCRegion, 90, 150);
            ho_ReviewRegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_ReviewRegion, out ho_ReviewRegionFillUp);
            ho_ReviewPCRegionFillUP.Dispose();
            HOperatorSet.FillUp(ho_ReviewPCRegion, out ho_ReviewPCRegionFillUP);
            ho_ReviewConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_ReviewRegionFillUp, out ho_ReviewConnectedRegions
                );
            ho_ReviewPCConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_ReviewPCRegionFillUP, out ho_ReviewPCConnectedRegions
                );
            ho_ReviewSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ReviewConnectedRegions, out ho_ReviewSelectedRegions,
                "area", "and", 300, 99999);
            ho_ReviewPCSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ReviewPCConnectedRegions, out ho_ReviewPCSelectedRegions,
                "area", "and", 500, 99999);
            //ho_RegionDilation.Dispose();
            //HOperatorSet.DilationRectangle1(ho_ReviewSelectedRegions, out ho_RegionDilation, 8, 8);
            //ho_RegionPCDilation.Dispose();
            //HOperatorSet.DilationRectangle1(ho_ReviewPCSelectedRegions, out ho_RegionPCDilation, 4, 4);
            //ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_ReviewSelectedRegions, out ho_RegionUnion);
            ho_RegionPCUnion.Dispose();
            HOperatorSet.Union1(ho_ReviewPCSelectedRegions, out ho_RegionPCUnion);
            HOperatorSet.AreaCenter(ho_RegionUnion, out hv_ReviewArea, out hv_ReviewRow,
                out hv_ReviewColumn);
            HOperatorSet.AreaCenter(ho_RegionPCUnion, out hv_ReviewPCArea, out hv_ReviewPCRow,
                out hv_ReviewPCColumn);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandleModel, "red");
            ho_ContoursVC.Dispose();
            HOperatorSet.GenContourRegionXld(ho_RegionUnion, out ho_ContoursVC, "border");
            ho_ContoursPC.Dispose();
            HOperatorSet.GenContourRegionXld(ho_RegionPCUnion, out ho_ContoursPC, "border");
            if ((int)(new HTuple(hv_ReviewColumn.TupleLess(hv_VCpartColumn))) != 0 && hv_ReviewArea > 0)
            {
                hv_ReviewVC = "OK";
            }
            else
            {
                hv_ReviewVC = "NG";
            }
            if ((int)(new HTuple(hv_ReviewPCRow.TupleLess(hv_PCPartRow))) != 0 && hv_ReviewPCArea > 0)
            {
                hv_ReviewPC = "OK";
            }
            else
            {
                hv_ReviewPC = "NG";
            }
            HOperatorSet.DispObj(ho_ContoursVC, hv_ExpDefaultWinHandleModel);
            HOperatorSet.DispObj(ho_ContoursPC, hv_ExpDefaultWinHandleModel);
            //disp_message (WindowHandle, 'ReviewColumn: ' + ReviewColumn + 'VCpartColumn: '+VCpartColumn, 'window', 50, 50, 'black', 'true')
            disp_message(hv_ExpDefaultWinHandleModel, "竖向电阻检测结果: " + hv_ReviewVC, "window", 55,
                12, "forest green", "true");
            disp_message(hv_ExpDefaultWinHandleModel, "横向电阻检测结果: " + hv_ReviewPC, "window", 75,
                12, "forest green", "true");
            if (hv_ReviewVC == "OK" && hv_ReviewPC == "OK")
            {
                ReviewResult = "OK";
            }
            else
            {
                ReviewResult = "NG";
            }
            ho_RectangleVerticalC.Dispose();
            ho_RectangleParallelC.Dispose();
            ho_ImageVCReduced.Dispose();
            ho_ImagePCRedeced.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_ImagePCEmphasize.Dispose();
            ho_ReviewRegion.Dispose();
            ho_ReviewPCRegion.Dispose();
            ho_ReviewRegionFillUp.Dispose();
            ho_ReviewPCRegionFillUP.Dispose();
            ho_ReviewConnectedRegions.Dispose();
            ho_ReviewPCConnectedRegions.Dispose();
            ho_ReviewSelectedRegions.Dispose();
            ho_ReviewPCSelectedRegions.Dispose();
            ho_RegionDilation.Dispose();
            ho_RegionPCDilation.Dispose();
            ho_RegionUnion.Dispose();
            ho_RegionPCUnion.Dispose();
            ho_ContoursVC.Dispose();
            ho_ContoursPC.Dispose();

            return ReviewResult;
        }



        public void dev_open_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
    HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {



            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = null, hv_ImageWidth = null, hv_ImageHeight = null;
            HTuple hv_TempWidth = null, hv_TempHeight = null, hv_WindowWidth = null;
            HTuple hv_WindowHeight = null;

            // Initialize local and output iconic variables 

            //This procedure opens a new graphics window and adjusts the size
            //such that it fits into the limits specified by WidthLimit
            //and HeightLimit, but also maintains the correct image aspect ratio.
            //
            //If it is impossible to match the minimum and maximum extent requirements
            //at the same time (f.e. if the image is very long but narrow),
            //the maximum value gets a higher priority,
            //
            //Parse input tuple WidthLimit
            if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
            {
                hv_MinWidth = 500;
                hv_MaxWidth = 800;
            }
            else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinWidth = 0;
                hv_MaxWidth = hv_WidthLimit.Clone();
            }
            else
            {
                hv_MinWidth = hv_WidthLimit[0];
                hv_MaxWidth = hv_WidthLimit[1];
            }
            //Parse input tuple HeightLimit
            if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(0))).TupleOr(
                new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
            {
                hv_MinHeight = 400;
                hv_MaxHeight = 600;
            }
            else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                1))) != 0)
            {
                hv_MinHeight = 0;
                hv_MaxHeight = hv_HeightLimit.Clone();
            }
            else
            {
                hv_MinHeight = hv_HeightLimit[0];
                hv_MaxHeight = hv_HeightLimit[1];
            }
            //
            //Test, if window size has to be changed.
            hv_ResizeFactor = 1;
            HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
            //First, expand window to the minimum extents (if necessary).
            if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                hv_ImageHeight)))) != 0)
            {
                hv_ResizeFactor = (((((hv_MinWidth.TupleReal()) / hv_ImageWidth)).TupleConcat(
                    (hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax();
            }
            hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
            //Then, shrink window to maximum extents (if necessary).
            if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                hv_TempHeight)))) != 0)
            {
                hv_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()) / hv_TempWidth)).TupleConcat(
                    (hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin());
            }
            hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
            hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
            //Resize window
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(hv_Row, hv_Column, hv_WindowWidth, hv_WindowHeight, 0, "", "", out hv_WindowHandle);
            HDevWindowStack.Push(hv_WindowHandle);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetPart(HDevWindowStack.GetActive(), 0, 0, hv_ImageHeight - 1, hv_ImageWidth - 1);
            }

            return;
        }


        // Short Description: This procedure writes a text message.
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {


            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Index = new HTuple(), hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_W = new HTuple();
            HTuple hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_Exception = new HTuple(), hv_CurrentColor = new HTuple();

            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 

            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If set to 'true', the text is written within a white box.
            //
            //prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //display text box depending on text size
            if ((int)(new HTuple(hv_Box.TupleEqual("true"))) != 0)
            {
                //calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                HOperatorSet.SetColor(hv_WindowHandle, "light gray");
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 3, hv_C1 + 3, hv_R2 + 3, hv_C2 + 3);
                HOperatorSet.SetColor(hv_WindowHandle, "white");
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            else if ((int)(new HTuple(hv_Box.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Box";
                throw new HalconException(hv_Exception);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        // Main procedure 
        private void action()
        { 
                HOperatorSet.ClearShapeModel(hv_KeyModelId);
                HOperatorSet.ClearShapeModel(hv_VerticalCModelId);
                HOperatorSet.ClearShapeModel(hv_ParallelCModelId);
                HOperatorSet.ClearShapeModel(hv_JKModelId);
                HOperatorSet.ClearShapeModel(hv_LModelId);
                //Matching 06: END of generated code for model application

            }
         
            
        }


    }