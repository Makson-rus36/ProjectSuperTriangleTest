using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using Brushes = System.Windows.Media.Brushes;

namespace ProjectSuperTriangleTest
{
    /// <summary>
    /// Логика взаимодействия для TeoryWindow.xaml
    /// </summary>
    public partial class TeoryWindow :  Window
    {
    
        private int _numOfVerticesLine;
        private int _uColorLocation;
        int currentPage = 0;

        List<Button> lb = new List<Button>();
        private XmlTextReader xmlReader;
        List<T_class> list;
        public TeoryWindow()
        {
            InitializeComponent();
            glControl_Load(null,null);
            PageTeory.Visibility = Visibility.Collapsed;
            Oglav.Visibility = Visibility.Visible;
             list = new List<T_class>();
           
            
           LoadTable(list);
            BtnCreate(list);
           

        }
        //Оглавление
        private void glControl_Load(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
           
            _numOfVerticesLine = InitVertexBuffersTriangle();
            GL.ClearColor(Color4.Black);
            glControl.BackColor = System.Drawing.Color.Transparent;
        }

        private void glControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            if (_numOfVerticesLine != 0)
            {
                GL.DrawArrays(PrimitiveType.Lines, 0, _numOfVerticesLine);
            }
            glControl.SwapBuffers();
        }
        
        

        private int InitVertexBuffersTriangleWithMediana()
        {
            float[] vertices = new float[]
            {
                //A
                 0.55f,0.85f,0.58f,0.95f,
                 0.58f,0.95f, 0.61f,0.85f,
                 0.56f,0.9f, 0.6f,0.9f,
                 0.5f, 0.8f, 0.5f,-0.8f,
                 //B
                 0.55f,-0.75f,0.58f,-0.75f,
                 0.58f,-0.75f,0.58f,-0.78f,
                 0.60f,-0.78f,0.55f,-0.78f,
                 0.60f,-0.78f,0.60f,-0.85f,
                 0.60f,-0.85f,0.55f,-0.85f,
                 0.55f,-0.75f,0.55f,-0.85f,

                 0.5f,-0.8f, -0.15f,0.0f,

                 //M
                 -0.05f,0.05f,-0.05f,-0.02f,
                 -0.05f,0.05f,-0.03f,-0.0025f,
                 -0.03f,-0.0025f,-0.01f,0.05f,
                 -0.01f,0.05f,-0.01f,-0.02f,

                 -0.15f,0.0f, 0.5f,0.8f,
                  0.5f,0.8f, -0.8f,0.8f,

                 //C
                 -0.82f,0.82f,-0.86f,0.82f,
                 -0.86f,0.82f,-0.86f,0.76f,
                 -0.86f,0.76f,-0.82f,0.76f,

                 -0.8f,0.8f, -0.15f,0.0f


            };
            int n = 99;

            int vbo;
            
            GL.GenBuffers(1, out vbo);

            // Get an array size in bytes
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            int sizeInBytes = vertices.Length * sizeof(float);
            // Send the vertex array to a video card memory
            GL.BufferData(BufferTarget.ArrayBuffer, sizeInBytes, vertices, BufferUsageHint.StaticDraw);
            // Config the aPosition variable
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            return n;
        }
        private int InitVertexBuffersTriangle()
        {
            float[] vertices = new float[]
            {
                //A
                 0.55f,0.85f,0.58f,0.95f,
                 0.58f,0.95f, 0.61f,0.85f,
                 0.56f,0.9f, 0.6f,0.9f,

                 0.5f, 0.8f, 0.5f,-0.8f,

                 //B
                 0.55f,-0.75f,0.58f,-0.75f,
                 0.58f,-0.75f,0.58f,-0.78f,
                 0.60f,-0.78f,0.55f,-0.78f,
                 0.60f,-0.78f,0.60f,-0.85f,
                 0.60f,-0.85f,0.55f,-0.85f,
                 0.55f,-0.75f,0.55f,-0.85f,

                 0.5f,-0.8f, -0.8f,0.8f,

                 

                 //C
                 -0.82f,0.82f,-0.86f,0.82f,
                 -0.86f,0.82f,-0.86f,0.76f,
                 -0.86f,0.76f,-0.82f,0.76f,

                 -0.8f,0.8f,  0.5f, 0.8f



            };
            int n = 99;

            int vbo;
            GL.GenBuffers(1, out vbo);

            // Get an array size in bytes
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            int sizeInBytes = vertices.Length * sizeof(float);
            // Send the vertex array to a video card memory
            GL.BufferData(BufferTarget.ArrayBuffer, sizeInBytes, vertices, BufferUsageHint.StaticDraw);
            // Config the aPosition variable
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            return n;
        }
        private int InitVertexBuffersTriangleWithHeight()
        {
            float[] vertices = new float[]
            {
                 //A
                 0.55f,0.85f,0.58f,0.95f,
                 0.58f,0.95f, 0.61f,0.85f,
                 0.56f,0.9f, 0.6f,0.9f,

                 0.5f, 0.8f, 0.5f,-0.8f,

                 //B
                 0.55f,-0.75f,0.58f,-0.75f,
                 0.58f,-0.75f,0.58f,-0.78f,
                 0.60f,-0.78f,0.55f,-0.78f,
                 0.60f,-0.78f,0.60f,-0.85f,
                 0.60f,-0.85f,0.55f,-0.85f,
                 0.55f,-0.75f,0.55f,-0.85f,

                 0.5f,-0.8f, -0.15f,0.0f,





                 //H
                 -0.05f,0.05f,-0.05f,-0.02f,
                 -0.05f,0.025f,-0.01f,0.025f,
                 -0.01f,0.055f,-0.01f,-0.02f,

                 //Значок высоты
                 -0.19f,0.04f,-0.14f,0.1f,
                 -0.14f,0.1f, -0.115f,0.062f,

                 -0.15f,0.0f, 0.5f,0.8f,
                  0.5f,0.8f, -0.8f,0.8f,

                 //C
                 -0.82f,0.82f,-0.86f,0.82f,
                 -0.86f,0.82f,-0.86f,0.76f,
                 -0.86f,0.76f,-0.82f,0.76f,

                 -0.8f,0.8f, -0.15f,0.0f
            };
            int n = 99;

            int vbo;
            GL.GenBuffers(1, out vbo);

            // Get an array size in bytes
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            int sizeInBytes = vertices.Length * sizeof(float);
            // Send the vertex array to a video card memory
            GL.BufferData(BufferTarget.ArrayBuffer, sizeInBytes, vertices, BufferUsageHint.StaticDraw);
            // Config the aPosition variable
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            return n;
        }
         private void BtnCreate(List<T_class> list)
         {
             for (int i = 0; i < list.Count; i++)
             {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri("Theme/TextControls.xaml", UriKind.RelativeOrAbsolute);

                Button button = new Button();
                 button.Height = 48;
                button.Style = (Style)dict["DefaultButtonStyle"];
                button.Background = Brushes.Transparent;
                button.Foreground = Brushes.White;
                button.BorderBrush = Brushes.Transparent;
                button.Margin = new Thickness(0, 0, 0, 5);
                Grid grid = new Grid();
                 ColumnDefinition column_icon = new ColumnDefinition();
                 column_icon.Width = new GridLength(48.00, GridUnitType.Pixel);
                 grid.ColumnDefinitions.Add(column_icon);
                 ColumnDefinition column_label = new ColumnDefinition();
                 column_label.Width = new GridLength(1, GridUnitType.Star);
                 grid.ColumnDefinitions.Add(column_label);
                 Label label = new Label();
                 label.Content = list[i].name_point;
                 label.FontSize = 27;
                label.Foreground = Brushes.White;
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                 image.Source = new BitmapImage(new Uri("IconMenuOglav.png", UriKind.RelativeOrAbsolute));
                 image.Stretch = Stretch.Uniform;
                 image.HorizontalAlignment = HorizontalAlignment.Left;
                 image.Margin = new Thickness(0);
                 grid.Children.Add(image);
                 int cp = i;
                 Grid.SetColumn(image, 0);
                 grid.Children.Add(label);
                 Grid.SetColumn(label, 1);
                 button.Content = grid;
                 button.HorizontalAlignment = HorizontalAlignment.Left;
                 stackTeory.Children.Add(button);
                 button.Click += (s, e) =>
                 {
                     Oglav.Visibility = Visibility.Collapsed;
                     PageTeory.Visibility = Visibility.Visible;
                     currentPage = 2;
                     ButtonNext_Click(null, null);
                     ButtonRedo_Click(null, null);
                     currentPage = cp;
                     fillpage();
                 };
                 lb.Add(button);
             }
         }

         private void LoadTable(List<T_class> list)
         {
            try
            {
                XmlTextReader reader = new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//TriangleSystem//TeoryXML.xml");
                int i = 0;
                String[] s = new string[3];
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text:
                            {
                                i++;
                                s[i - 1] = reader.Value;
                                break;
                            }
                    }
                    if (i == 3)
                    {
                        list.Add(new T_class(s[0], s[1], s[2]));
                        i = 0;
                    }

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
         }
        



        //Блок страницы Теории
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            Oglav.Visibility = Visibility.Collapsed;
            PageTeory.Visibility = Visibility.Visible;
            GL.Clear(ClearBufferMask.AccumBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.CoverageBufferBitNv);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.StencilBufferBit);
            GL.ClearColor(Color4.Black);
            fillpage();
            
        }
        private void fillpage()
        {
            GL.Clear(ClearBufferMask.AccumBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.CoverageBufferBitNv);
            GL.ClearColor(Color4.Black);
            switch (list[currentPage].type)
            {
                case "обычный":
                    {
                        _numOfVerticesLine = InitVertexBuffersTriangle();
                        break;
                    }
                case "высота":
                    {
                        _numOfVerticesLine = InitVertexBuffersTriangleWithHeight();
                        break;
                    }
                case "медиана":
                    {
                        _numOfVerticesLine = InitVertexBuffersTriangleWithMediana();
                        break;
                    }
            }
            
            glControl_Paint(null, null);
            labelName.Text = list[currentPage].name_point;
            labeDescr.Text = list[currentPage].descript;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            if (currentPage < list.Count)
            {
                GL.Clear(ClearBufferMask.AccumBufferBit);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Clear(ClearBufferMask.CoverageBufferBitNv);
                GL.ClearColor(Color4.Black);
                fillpage();
            }
            else
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Вы прошли теорию.\nДля того чтобы пройти тест нажмите Yes\n Для выхода в оглавление нажмите NO" +
                    "\n Чтобы остаться нажмите Cancel", "Конец",
                                                                                                          System.Windows.Forms.MessageBoxButtons.YesNoCancel,
                                                                                                          System.Windows.Forms.MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes) {
                    this.Close();
                    RegWindow window1 = new RegWindow();
                    window1.Show();
                }
                    
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    PageTeory.Visibility = Visibility.Collapsed;
                    Oglav.Visibility = Visibility.Visible;
                }

                currentPage--;
            }
        }

        private void ButtonRedo_Click(object sender, RoutedEventArgs e)
        {

            if (currentPage > 0)
            {
                currentPage--;
                GL.Clear(ClearBufferMask.AccumBufferBit);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Clear(ClearBufferMask.CoverageBufferBitNv);
                GL.ClearColor(Color4.Black);
                glControl_Load(null, null);
                fillpage();

            }
            else
            {
                MessageBox.Show("Вы в начале");
                currentPage--;
            }
        }

        private void ButtonBackToOgla_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 0;
            fillpage();
            GL.Clear(ClearBufferMask.AccumBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.CoverageBufferBitNv);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.StencilBufferBit);
            GL.ClearColor(Color4.Black);
            
            PageTeory.Visibility = Visibility.Collapsed;
            Oglav.Visibility = Visibility.Visible;
        }

        private void BtnClickExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Подтвердите выход", "Выход",
                                                                                                         System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                                                         System.Windows.Forms.MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
    public class T_class{
        public String name_point;
       public String descript;
        public String type;
         public T_class(String name, String Desc,String type)
        {
            this.name_point = name;
            this.descript = Desc;
            this.type = type;
        }
    }
}
