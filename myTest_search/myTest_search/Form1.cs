using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// halcon 库函数
using HalconDotNet;

namespace myTest_search
{
    public partial class Form1 : Form
    {
        // 主窗体类和模板类
        HDevelopExport HD = new HDevelopExport();
        HDevelopExportModel HDModel = new HDevelopExportModel();
        // 模板存储数组
        HTuple [] modelHandleID = new HTuple [5];
        // 相机句柄
        public HTuple hv_AcqHandle;
        public HTuple hv_AcqHandle00;
        public HTuple hv_AcqHandle01;
        public HTuple hv_AcqHandle02;
        // 每次接收到信号后读取到的image
        public HObject ImageSignalRead;
        public HObject ImageRead00;
        public HObject ImageRead01;
        public HObject ImageRead02;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //  MessageBox.Show("欢迎使用本软件");
        }

        private void StartProcessButtons(object sender, EventArgs e)
        {
            // 开始检测按钮，只打开相机驱动
            HObject imageTemp = null;
 //           hv_AcqHandle = HD.OpenCamera(hWindowControl1.HalconWindow);
            hv_AcqHandle00 = HD.OpenCamera00(hWindowControl3.HalconWindow); //右侧第一个小窗口句柄，绑定0号相机
            HOperatorSet.GrabImageAsync(out imageTemp, hv_AcqHandle00, -1);
            hv_AcqHandle01 = HD.OpenCamera01(hWindowControl4.HalconWindow); //右侧第二个小窗口句柄，绑定1号相机
            hv_AcqHandle02 = HD.OpenCamera02(hWindowControl5.HalconWindow); //右侧第三个小窗口句柄，绑定2号相机
        }

        private void GetSignalButtons(object sender, EventArgs e)
        {
            // 暂定的信号源，后期需进行改进，方案：1、循环读取txt文件 2、TCP通信
            // 由于模板的建立有顺序，故可将模板句柄按顺序存储
            HTuple resultNum = null, reviewResult = null, lastResult = null;
            HTuple []LittleCameraReview = new HTuple [4];
            for (int i = 0; i < 5; i++)
            {
                modelHandleID[i] = i;
            }
            HTuple hv_cameraSignalFlag = 1;     // 定义信号源为ture,即按下button后相当给一个信号
            //HD.StartProcessPicture(hWindowControl1.HalconWindow, hv_AcqHandle, hv_cameraSignalFlag);
            // 读取主相机的图像
//            ImageSignalRead = HDModel.GetImageFromCamera(hWindowControl1.HalconWindow, hv_AcqHandle);
            // 拿到三个小相机获得的图像
            ImageRead00 = HDModel.GetImageFromCamera(hWindowControl3.HalconWindow, hv_AcqHandle00);
            ImageRead01 = HDModel.GetImageFromCamera(hWindowControl4.HalconWindow, hv_AcqHandle01);
            ImageRead02 = HDModel.GetImageFromCamera(hWindowControl5.HalconWindow, hv_AcqHandle02);
            // 主相机对电路板进行拍摄后得到的匹配结果
//            resultNum = HDModel.ModelMatching(modelHandleID, hWindowControl1.HalconWindow, hv_cameraSignalFlag, hv_AcqHandle, ImageSignalRead);
            // 主相机对电路板拍摄后进行的电容极性检测，两个电容，竖向与横向结果进行与操作后得到 reviewResult
//            reviewResult = HDModel.CapacityReview(hWindowControl1.HalconWindow);
            // 三个小相机对电路板极性的检测
            LittleCameraReview = HDModel.ProcessLittleCamera(hWindowControl3.HalconWindow, hWindowControl4.HalconWindow, hWindowControl5.HalconWindow);
            textBox2.Text = LittleCameraReview[1].ToString();
            textBox3.Text = LittleCameraReview[2].ToString();
            textBox4.Text = LittleCameraReview[3].ToString();
            if (resultNum == "OK" && reviewResult == "OK")
            {
                lastResult = "OK";
            }
            else
            {
                lastResult = "NG";
            }
            textBox1.Text = lastResult.ToString();
            hv_cameraSignalFlag = 0;            // 信号源重置为flase
        }

        public void DealCameraImage00(HTuple windows, HTuple ImageRead00)
        {

        }


        private void StopProcessButtons(object sender, EventArgs e)
        {
            // 停止检测,将上次检测的图片显示出来
            HD.DispImageToHWindow(hWindowControl1.HalconWindow, ImageSignalRead);
            HD.CloseProcess(hv_AcqHandle, hv_AcqHandle00, hv_AcqHandle01, hv_AcqHandle02, hWindowControl1.HalconWindow, hWindowControl3.HalconWindow,
                hWindowControl4.HalconWindow, hWindowControl5.HalconWindow);
        }

        private void ModelOperationFun(object sender, EventArgs e)
        {
            // 模板操作按钮，在原有窗体基础上新打开一个窗体
            ModelOperation modelForm = new ModelOperation();
            modelForm.ShowDialog();
        }

        private void exitButton(object sender, EventArgs e)
        {
            // 退出按钮
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 关闭所有的线程
               // this.Dispose();
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // null
        }

        private void MarkSearchButton(object sender, EventArgs e)
        {
            // 进行mark点检测，用来确定坐标系
            HD.MarkPointSearch(hWindowControl1.HalconWindow, ImageSignalRead);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void hWindowControl2_HMouseMove(object sender, HMouseEventArgs e)
        {

        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }



    public partial class HDevelopExport
    {
        public HTuple hv_ExpDefaultWinHandle;
        public HTuple hv_ExpDefaultWinHandleWindows00;
        public HTuple hv_ExpDefaultWinHandleWindows01;
        public HTuple hv_ExpDefaultWinHandleWindows02;
        // Local iconic variables 
        HObject ho_Image = null, ho_Rectangle = null, ho_ImageReduced = null;
        HObject ho_ImagePart = null, ho_GrayImage = null, ho_ImageEmphasize = null;
        HObject ho_Region = null, ho_ConnectedRegions = null, ho_SelectedRegions = null;
        HObject ho_RegionFillUp = null;

        // changed at 17/02/25 11:11
        // 为解决窗体之间传递数据问题，将创建小相机模板的程序放到主窗体中，防止传值时的数据丢失
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

        // Local control variables 


        HTuple hv_AcqHandle = null, hv_cameraNumFlag = null;
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_WindowHandle = new HTuple(), hv_Row1 = new HTuple();
        HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
        HTuple hv_Column2 = new HTuple(), hv_Start = new HTuple();
        HTuple hv_Width1 = new HTuple(), hv_Height1 = new HTuple();
        HTuple hv_Number = new HTuple(), hv_Stop = new HTuple();
        HTuple hv_Duration1 = new HTuple(), hv_Duration2 = new HTuple(), hv_result = new HTuple();

        // add at 17/02/24 9:36  增加三个小相机的检测程序
        // 初始化相机变量
        HTuple hv_AcqHandle00 = null, hv_AcqHandle01 = null, hv_AcqHandle02 = null;
        public HTuple OpenCamera00(HTuple Windows)
        {
            hv_ExpDefaultWinHandle = Windows;
            disp_message(hv_ExpDefaultWinHandle, "相机正在初始化，请稍候", "window", 12,
                    12, "black", "true");
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
       -1, "false", "default", "0", 0, -1, out hv_AcqHandle00);
            HOperatorSet.GrabImageStart(hv_AcqHandle00, -1);
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            disp_message(hv_ExpDefaultWinHandle, "初始化完成，等待检测信号", "window", 12,
                    12, "black", "true");
            return hv_AcqHandle00;
        }

        public HTuple OpenCamera01(HTuple Windows)
        {
            hv_ExpDefaultWinHandle = Windows;
            disp_message(hv_ExpDefaultWinHandle, "相机正在初始化，请稍候", "window", 12,
                    12, "black", "true");
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
       -1, "false", "default", "1", 0, -1, out hv_AcqHandle01);
            HOperatorSet.GrabImageStart(hv_AcqHandle01, -1);
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            disp_message(hv_ExpDefaultWinHandle, "初始化完成，等待检测信号", "window", 12,
                    12, "black", "true");
            return hv_AcqHandle01;
        }

        public HTuple OpenCamera02(HTuple Windows)
        {
            hv_ExpDefaultWinHandle = Windows;
            disp_message(hv_ExpDefaultWinHandle, "相机正在初始化，请稍候", "window", 12,
                    12, "black", "true");
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
       -1, "false", "default", "2", 0, -1, out hv_AcqHandle02);
            HOperatorSet.GrabImageStart(hv_AcqHandle02, -1);
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            disp_message(hv_ExpDefaultWinHandle, "初始化完成，等待检测信号", "window", 12,
                    12, "black", "true");
            return hv_AcqHandle02;
        }

        public void HDevelopStop()
        {

        }

        // Procedures 
        // Chapter: Graphics / Text
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



        public HTuple OpenCamera(HTuple Windows)
        {
            // 调用相机驱动，获取相机句柄
            hv_ExpDefaultWinHandle = Windows;
            disp_message(hv_ExpDefaultWinHandle, "相机正在初始化，请稍候", "window", 12,
                    12, "black", "true");
            HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8,
          "rgb", -1, "false", "default", "DMx 72AUC02", 0, -1, out hv_AcqHandle);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
           // System.Threading.Thread.Sleep(1000);
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            disp_message(hv_ExpDefaultWinHandle, "初始化完成，等待检测信号", "window", 12,
                    12, "black", "true");
            return hv_AcqHandle;
        }

        public void ModelOperation(HTuple Windows)
        {

        }

        public void StartProcessPicture(HTuple Windows, HTuple hv_AcqHandle, int hv_cameraSignalFlag)
        {
            hv_ExpDefaultWinHandle = Windows;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImagePart);
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);

            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "green");
            hv_result = "NULL";
            while (hv_cameraSignalFlag == 1)
            {
                ho_Image.Dispose();
                // 获取图像 ho_Image, 并计算运行时间1
                HOperatorSet.CountSeconds(out hv_Start);
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                //Image Acquisition 01: Do something
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.CountSeconds(out hv_Stop);
                hv_Duration1 = (hv_Stop - hv_Start) * 1000;
                HOperatorSet.SetPart(hv_ExpDefaultWinHandle, 0, 0, hv_Height - 1, hv_Width - 1);
                // 将拿到的图像进行显示，并交互画出ROI
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
                disp_message(hv_ExpDefaultWinHandle, "在窗口上画一个矩形检测区域", "window", 12,
                    12, "black", "true");
                HOperatorSet.DrawRectangle1(hv_ExpDefaultWinHandle, out hv_Row1, out hv_Column1,
                    out hv_Row2, out hv_Column2);
                ho_Rectangle.Dispose();
                // 开始计算时间2
                HOperatorSet.CountSeconds(out hv_Start);
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2,
                    hv_Column2);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
                ho_ImagePart.Dispose();
                HOperatorSet.CropDomain(ho_ImageReduced, out ho_ImagePart);
                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(ho_ImagePart, out ho_GrayImage);
                HOperatorSet.GetImageSize(ho_GrayImage, out hv_Width1, out hv_Height1);
                ho_ImageEmphasize.Dispose();
                HOperatorSet.Emphasize(ho_GrayImage, out ho_ImageEmphasize, hv_Width1, hv_Height1,
                    2);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Region, 128, 255);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_Region, out ho_RegionFillUp);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionFillUp, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 150, 99999);
                hv_cameraNumFlag = 0;
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleGreater(0))) != 0)
                {
                    hv_cameraNumFlag = 1;
                }
                HOperatorSet.CountSeconds(out hv_Stop);
                hv_Duration2 = (hv_Stop - hv_Start) * 1000;
                hv_Duration1 += hv_Duration2;
                if (hv_cameraNumFlag == 1)
                {
                    hv_result = "OK";
                }
                else
                {
                    hv_result = "NG";
                }
                //
                //Display results and the time needed

                disp_message(hv_ExpDefaultWinHandle, ((("检测时间：" + (hv_Duration1.TupleString("3.0f"))) + " ms")).TupleConcat(
                    "相机检测结果： " + hv_result), "window", 12, 12, "forest green", "true");

                // HDevelopStop();
                // 将信号清除，退出此次循环
                hv_cameraSignalFlag = 0;
            }
        }

        public void DispImageToHWindow(HTuple window, HObject DispImage)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.DispObj(DispImage, hv_ExpDefaultWinHandle);
        }

        public void MarkPointSearch(HTuple Windows, HObject MarkImage)
        {
           // DispImageToHWindow(Windows, MarkImage);
            hv_ExpDefaultWinHandle = Windows;
            HObject ho_Image1, ho_Region, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_SelectedRegions1, ho_Cross;


            // Local control variables 
            HTuple hv_Area = null;
            HTuple hv_Row = null, hv_Column = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            ho_Image1.Dispose();
            ho_Image1 = MarkImage;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            //dev_open_window_fit_image(ho_Image1, 0, 0, -1, -1, out hv_ExpDefaultWinHandle);
            HOperatorSet.DispObj(ho_Image1, hv_ExpDefaultWinHandle);
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_Image1, out ho_Region, 0, 80);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "circularity",
                "and", 0.6, 1);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions1, "area",
                "and", 1000, 99999);
            HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area, out hv_Row, out hv_Column);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "green");
            ho_Cross.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 200, (new HTuple(90)).TupleRad());
            HOperatorSet.DispObj(ho_Cross, hv_ExpDefaultWinHandle);
            //ho_Image1.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_Cross.Dispose();
        }

        public void CloseProcess(HTuple hv_AcqHandle, HTuple hv_AcqHandle00, HTuple hv_AcqHandle01, HTuple hv_AcqHandle02, HTuple Windows
            , HTuple Windows00, HTuple Windows01, HTuple Windows02)
        {
            // 在停止检测时关闭相机驱动
            hv_ExpDefaultWinHandle = Windows;   //上层主相机窗口句柄
            // 三个小相机窗口句柄
            hv_ExpDefaultWinHandleWindows00 = Windows00;
            hv_ExpDefaultWinHandleWindows01 = Windows01;
            hv_ExpDefaultWinHandleWindows02 = Windows02;
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle00);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle01);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle02);
           // HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
           // HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
            disp_message(hv_ExpDefaultWinHandle, "检测结束", "window", 12, 12, "black", "true");
            disp_message(hv_ExpDefaultWinHandleWindows00, "检测结束", "window", 12, 12, "black", "true");
            disp_message(hv_ExpDefaultWinHandleWindows01, "检测结束", "window", 12, 12, "black", "true");
            disp_message(hv_ExpDefaultWinHandleWindows02, "检测结束", "window", 12, 12, "black", "true");
        }
        public void dev_open_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column, HTuple hv_WidthLimit, 
            HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
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
    }
}




      






    
