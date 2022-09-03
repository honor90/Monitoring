using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using System.Linq.Expressions;

namespace MonitorMechanics
{
    public class Axis
    {
        ZedGraphControl ZGC_axis;

        public String TITLE;
        public String NAME_X;
        public String NAME_Y;

        public double X_MIN_LIMIT;
        public double X_MAX_LIMIT;

        public double Y_MIN_LIMIT;
        public double Y_MAX_LIMIT;

        public int SIZE_TEXT = 1;

        private double delta;

        


        public Axis(ZedGraphControl ZGC)
        {

            // Получим панель для рисования
            ZGC_axis = ZGC;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            ZGC_axis.GraphPane.CurveList.Clear();
        }

        public void GreateAxis()
        {
            // Изменим текст заголовка графика
            ZGC_axis.GraphPane.Title.Text = TITLE;
            // Изменим тест надписи по оси X
            ZGC_axis.GraphPane.XAxis.Title.Text = NAME_X;
            // Изменим текст по оси Y
            ZGC_axis.GraphPane.YAxis.Title.Text = NAME_Y;

            //ZGC_axis.GraphPane.YAxis.Title.FontSpec.Size = 3.0F;

            ZGC_axis.GraphPane.XAxis.Title.FontSpec.IsUnderline = true;
            ZGC_axis.GraphPane.YAxis.Title.FontSpec.IsUnderline = true;

            ZGC_axis.GraphPane.XAxis.Title.FontSpec.IsBold = false;
            ZGC_axis.GraphPane.YAxis.Title.FontSpec.IsBold = false;

            //ZGC_axis.GraphPane.YAxis.Title.FontSpec.S

            //Отключим рамку 
            ZGC_axis.GraphPane.Border.IsVisible = false;

            // Включаем отображение сетки напротив крупных рисок 
            ZGC_axis.GraphPane.XAxis.MajorGrid.IsVisible = true;
            ZGC_axis.GraphPane.YAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            ZGC_axis.GraphPane.XAxis.MajorGrid.DashOn = 5;

            // затем 5 пикселей - пропуск
            ZGC_axis.GraphPane.XAxis.MajorGrid.DashOff = 5;


            ZGC_axis.GraphPane.YAxis.MajorGrid.DashOn = 5;

            // затем 5 пикселей - пропуск
            ZGC_axis.GraphPane.YAxis.MajorGrid.DashOff = 5;


            //Формат по си Х
            //ZGC_axis.GraphPane.XAxis.Type = AxisType.Date;
            //ZGC_axis.GraphPane.XAxis.Scale.Format = "HH:mm:ss";

            // Устанавливаем интересующий нас интервал по оси X
            ZGC_axis.GraphPane.XAxis.Scale.Min = X_MIN_LIMIT;
            ZGC_axis.GraphPane.XAxis.Scale.Max = X_MAX_LIMIT;


            // Устанавливаем интересующий нас интервал по оси Y
            ZGC_axis.GraphPane.YAxis.Scale.Min = Y_MIN_LIMIT;
            ZGC_axis.GraphPane.YAxis.Scale.Max = Y_MAX_LIMIT;


            //ZGC_axis.GraphPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            //ZGC_axis.GraphPane.XAxis.Scale.MinorUnit = DateUnit.Second;

            //ZGC_axis.GraphPane.XAxis.Type = AxisType.DateAsOrdinal;

            //ZGC_axis.GraphPane.XAxis.Scale.MajorStep = 10;
            //ZGC_axis.GraphPane.XAxis.Scale.MinorStep = 5;


            // Установим размеры шрифтов для меток вдоль осей
            ZGC_axis.GraphPane.XAxis.Scale.FontSpec.Size = SIZE_TEXT;
            ZGC_axis.GraphPane.YAxis.Scale.FontSpec.Size = SIZE_TEXT;

            // Установим размеры шрифтов для подписей по осям
            ZGC_axis.GraphPane.XAxis.Title.FontSpec.Size = SIZE_TEXT;
            ZGC_axis.GraphPane.YAxis.Title.FontSpec.Size = SIZE_TEXT;


            ZGC_axis.GraphPane.XAxis.Scale.MajorStep = X_MAX_LIMIT / 12;
            ZGC_axis.GraphPane.XAxis.Scale.MinorStep = X_MAX_LIMIT / 12;

            ZGC_axis.GraphPane.YAxis.Scale.MajorStep = Y_MAX_LIMIT / 10;
            ZGC_axis.GraphPane.YAxis.Scale.MinorStep = Y_MAX_LIMIT / 10;

            delta = (X_MAX_LIMIT - X_MIN_LIMIT) / (X_MAX_LIMIT * 10);

            // Подпишемся на событие, которое срабатывает при изменении курсора
            ZGC_axis.CursorChanged += new EventHandler(zedGraph_CursorChanged);
            // Подпишемся на сообщение, уведомляющее о том,
            // что пользователь изменяет масштаб графика
            ZGC_axis.ZoomEvent += new ZedGraphControl.ZoomEventHandler(zedGraph_ZoomEvent);
        }

        void zedGraph_CursorChanged(object sender, EventArgs e)
        {
            // Разрешим ZedGraph'у изменять курсор при выделении участка на графике,
            // а также при перемещении графика.
            // В обоих случаях курсор будет "захвачен"
            // Если курсор не "захвачен", то установим курсор обратно в виде стрелки.
            // Если нужно запретить изменять курсор в любом случае,
            // то достаточно просто убрать условие.

            //if (!ZGC_axis.Capture)
            //{
                ZGC_axis.Cursor = Cursors.Arrow;
            //}
        }

        // Обработчик события при изменении масштаба
        void zedGraph_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            // Для простоты примера будем ограничивать масштабирование
            // только в сторону уменьшения размера графика

            // Проверим интервал для каждой оси и
            // при необходимости скорректируем его

            //if (ZGC_axis.GraphPane.XAxis.Scale.Min <= X_MIN_LIMIT)
            //{
                ZGC_axis.GraphPane.XAxis.Scale.Min = X_MIN_LIMIT;
            //}

            //if (ZGC_axis.GraphPane.XAxis.Scale.Max >= X_MAX_LIMIT)
            //{
                ZGC_axis.GraphPane.XAxis.Scale.Max = X_MAX_LIMIT;
            //}

            //if (ZGC_axis.GraphPane.YAxis.Scale.Min <= Y_MIN_LIMIT)
            //{
                ZGC_axis.GraphPane.YAxis.Scale.Min = Y_MIN_LIMIT;
            //}

            //if (ZGC_axis.GraphPane.YAxis.Scale.Max >= Y_MAX_LIMIT || ZGC_axis.GraphPane.YAxis.Scale.Max < Y_MAX_LIMIT)
            //{
                ZGC_axis.GraphPane.YAxis.Scale.Max = Y_MAX_LIMIT;
            //}
        }

        private int count_point = 0;

        public void UpdateAxis()
        {
            if (count_point <= 120) count_point += 1;

            if (count_point > 120)
            {
                ZGC_axis.GraphPane.XAxis.Scale.Min += delta;
                ZGC_axis.GraphPane.XAxis.Scale.Max += delta;
            }

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            ZGC_axis.AxisChange();
        }

        public void Clear()
        {
            ZGC_axis.GraphPane.CurveList.Clear();
        }
    }

    public class Line
    {
        private ZedGraphControl ZGC_line;

        public Color ColorLine = Color.Green;
        public String NameLine = "График №1";


        List<double> _data;

        int _capacity = 600;

        public Line(ZedGraphControl ZGC)
        {
            ZGC_line = ZGC;

            _data = new List<double>();

        }


        public void CreateLine()
        {
            AddPoint(0, 0);

            ZGC_line.GraphPane.Legend.Position = ZedGraph.LegendPos.InsideTopRight;
            ZGC_line.GraphPane.Legend.FontSpec.Size = 10;
            ZGC_line.GraphPane.Legend.Border.IsVisible = false;

        }


        public void AddPoint(double point_X, double point_Y)
        {
            _data.Add(point_Y);

            if (_data.Count > _capacity)
            {
                _data.RemoveAt(0);
            }
        }

        public void ClearPane()
        {
            ZGC_line.GraphPane.CurveList.Clear();
        }

        public void DrawLine(ushort point1)
        {
            PointPairList list_line = new PointPairList();

            list_line.Add(0, point1);
            list_line.Add(59.9, point1);
            ZGC_line.GraphPane.AddCurve("Порог", list_line, Color.Red, SymbolType.None);
        }

        public void UpdateLine()
        {
            double dx = 0.1;

            double curr_x = 0;

            // Создадим список точек
            PointPairList list = new PointPairList();

            
            foreach (double val in _data)
            {
                list.Add(curr_x, val);
                curr_x += dx;
            }

            //ZGC_line.GraphPane.CurveList.Clear();
            ZGC_line.GraphPane.AddCurve(NameLine, list, ColorLine, SymbolType.None);
            
            ZGC_line.GraphPane.AxisChange();
            // Обновляем график
            ZGC_line.Invalidate();
        }
    }
 }
