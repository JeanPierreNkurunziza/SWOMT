using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps;

namespace SWOMT.Views
{
   public class PrintManager
    {
        public void printManager(DataGrid dataGrid, string title)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                FlowDocument fd = new FlowDocument();

                Paragraph p = new Paragraph(new Run(title));
                p.FontStyle = dataGrid.FontStyle;
                p.FontFamily = dataGrid.FontFamily;
                p.FontSize = 18;
                fd.Blocks.Add(p);

                Table table = new Table();
                TableRowGroup tableRowGroup = new TableRowGroup();
                TableRow r = new TableRow();
                fd.PageWidth = printDialog.PrintableAreaWidth;
                fd.PageHeight = printDialog.PrintableAreaHeight;
                fd.BringIntoView();

                fd.TextAlignment = TextAlignment.Center;
                fd.ColumnWidth = 500;
                table.CellSpacing = 0;

                var headerList = dataGrid.Columns.Select(e => e.Header.ToString()).ToList();
                List<dynamic> bindList = new List<dynamic>();

                for (int j = 0; j < headerList.Count; j++)
                {
                    r.Cells.Add(new TableCell(new Paragraph(new Run(headerList[j]))));
                    r.Cells[j].ColumnSpan = 4;
                    r.Cells[j].Padding = new Thickness(4);
                    r.Cells[j].BorderBrush = Brushes.Black;
                    r.Cells[j].FontWeight = FontWeights.Bold;
                    r.Cells[j].Background = Brushes.DarkGray;
                    r.Cells[j].Foreground = Brushes.White;
                    r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);

                    var binding = (dataGrid.Columns[j] as DataGridBoundColumn).Binding as Binding;
                    bindList.Add(binding.Path.Path);
                }

                tableRowGroup.Rows.Add(r);
                table.RowGroups.Add(tableRowGroup);

                for (int i = 0; i < dataGrid.Items.Count; i++)
                {
                    dynamic row;

                    if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                    { row = (DataRowView)dataGrid.Items.GetItemAt(i); }
                    else
                    {
                        row = dataGrid.Items.GetItemAt(i);
                    }

                    table.BorderBrush = Brushes.Gray;
                    table.BorderThickness = new Thickness(1, 1, 0, 0);
                    table.FontStyle = dataGrid.FontStyle;
                    table.FontFamily = dataGrid.FontFamily;
                    table.FontSize = 13;
                    tableRowGroup = new TableRowGroup();
                    r = new TableRow();

                    for (int j = 0; j < dataGrid.Items.Count; j++)
                    {
                        if (dataGrid.ItemsSource.ToString().ToLower() == "system.data.linqdataview")
                        {
                            r.Cells.Add(new TableCell(new Paragraph(new Run(row.Row.ItemArray[j].ToString()))));
                        }
                        else
                        {
                             r.Cells.Add(new TableCell(new Paragraph(new Run(row.GetType().GetProperty(bindList[j]).GetValue(row, null).ToString())))); 
                           // r.Cells.Add(new TableCell(new Paragraph(new Run(row.Row.ItemArray[j].ToString()))));
                           
                        }

                        r.Cells[j].ColumnSpan = 4;
                        r.Cells[j].Padding = new Thickness(4);
                        r.Cells[j].BorderBrush = Brushes.DarkGray;
                        r.Cells[j].BorderThickness = new Thickness(0, 0, 1, 1);
                    }

                    tableRowGroup.Rows.Add(r);
                    table.RowGroups.Add(tableRowGroup);
                }

                fd.Blocks.Add(table);
                printDialog.PrintDocument(((IDocumentPaginatorSource)fd).DocumentPaginator, "");
            }
        }
        ////public const double cm = 37;
        ////public double Margin = 0.5 * cm;
        ////public double PageWidth = 21 * cm;
        ////public double PageHeight = 29 * cm;
        ////public double RowHeight = 0.7 * cm;
        ////public bool PageNumberVisibility = true;
        ////public bool DateVisibility = true;
        ////public double FontSize = 14;
        ////public double HeaderFontSize = 14;
        ////public bool IsBold = false;
        ////public void PrintDataGrid(FrameworkElement header, DataGrid grid, FrameworkElement footer, PrintDialog printDialog)
        ////{
        ////    if (header == null) { header = new FrameworkElement(); header.Width = 1; header.Height = 1; }
        ////    if (footer == null) { footer = new FrameworkElement(); footer.Width = 1; footer.Height = 1; }
        ////    if (grid == null) return;

        ////    Size pageSize = new Size(PageWidth, PageHeight);

        ////    FixedDocument fixedDoc = new FixedDocument();
        ////    fixedDoc.DocumentPaginator.PageSize = pageSize;

        ////    double GridActualWidth = grid.ActualWidth == 0 ? grid.Width : grid.ActualWidth;

        ////    double PageWidthWithMargin = pageSize.Width - Margin * 2;
        ////    double PageHeightWithMargin = pageSize.Height - Margin * 2;



        ////    // scale the header  
        ////    double headerScale = (header?.Width ?? 0) / PageWidthWithMargin;
        ////    double headerWidth = PageWidthWithMargin;
        ////    double headerHeight = (header?.Height ?? 0) * headerScale;
        ////    header.Height = headerHeight;
        ////    header.Width = headerWidth;
        ////    // scale the footer  
        ////    double footerScale = (footer?.Width ?? 0) / PageWidthWithMargin;
        ////    double footerWidth = PageWidthWithMargin;
        ////    double footerHeight = (footer?.Height ?? 0) * footerScale;
        ////    footer.Height = footerHeight;
        ////    footer.Width = footerWidth;

        ////    int pageNumber = 1;
        ////    string Now = DateTime.Now.ToShortDateString();

        ////    //add the header 
        ////    FixedPage fixedPage = new FixedPage();
        ////    fixedPage.Background = Brushes.White;
        ////    fixedPage.Width = pageSize.Width;
        ////    fixedPage.Height = pageSize.Height;

        ////    FixedPage.SetTop(header, Margin);
        ////    FixedPage.SetLeft(header, Margin);

        ////    fixedPage.Children.Add(header);
        ////    // its like cursor for current page Height to start add grid rows
        ////    double CurrentPageHeight = headerHeight + 1 * cm;
        ////    int lastRowIndex = 0;
        ////    bool IsFooterAdded = false;

        ////    for (; ; )
        ////    {
        ////        int AvaliableRowNumber;


        ////        var SpaceNeededForRestRows = (CurrentPageHeight + (grid.Items.Count - lastRowIndex) * RowHeight);

        ////        //To avoid printing the footer in a separate page
        ////        if (SpaceNeededForRestRows > (pageSize.Height - footerHeight - Margin) && (SpaceNeededForRestRows < (pageSize.Height - Margin)))
        ////            AvaliableRowNumber = (int)((pageSize.Height - CurrentPageHeight - Margin - footerHeight) / RowHeight);
        ////        // calc the Avaliable Row acording to CurrentPageHeight
        ////        else AvaliableRowNumber = (int)((pageSize.Height - CurrentPageHeight - Margin) / RowHeight);

        ////        // create new page except first page cause we created it prev
        ////        if (pageNumber > 1)
        ////        {
        ////            fixedPage = new FixedPage();
        ////            fixedPage.Background = Brushes.White;
        ////            fixedPage.Width = pageSize.Width;
        ////            fixedPage.Height = pageSize.Height;
        ////        }

        ////        // create new data grid with  columns width and binding
        ////        DataGrid gridToAdd;
        ////        gridToAdd = GetDataGrid(grid, GridActualWidth, PageWidthWithMargin);


        ////        FixedPage.SetTop(gridToAdd, CurrentPageHeight); // top margin
        ////        FixedPage.SetLeft(gridToAdd, Margin); // left margin

        ////        // add the avaliable rows to the cuurent grid 
        ////        for (int i = lastRowIndex; i < grid.Items.Count && i < AvaliableRowNumber + lastRowIndex; i++)
        ////        {
        ////            gridToAdd.Items.Add(grid.Items[i]);
        ////        }
        ////        lastRowIndex += gridToAdd.Items.Count + 1;

        ////        // add date
        ////        TextBlock dateText = new TextBlock();
        ////        if (DateVisibility) dateText.Visibility = Visibility.Visible;
        ////        else dateText.Visibility = Visibility.Hidden;
        ////        dateText.Text = Now;

        ////        // add page number
        ////        TextBlock PageNumberText = new TextBlock();
        ////        if (PageNumberVisibility) PageNumberText.Visibility = Visibility.Visible;
        ////        else PageNumberText.Visibility = Visibility.Hidden;
        ////        PageNumberText.Text = "Page : " + pageNumber;

        ////        FixedPage.SetTop(dateText, PageHeightWithMargin);
        ////        FixedPage.SetLeft(dateText, Margin);

        ////        FixedPage.SetTop(PageNumberText, PageHeightWithMargin);
        ////        FixedPage.SetLeft(PageNumberText, PageWidthWithMargin - PageNumberText.Text.Length * 10);

        ////        fixedPage.Children.Add(gridToAdd);
        ////        fixedPage.Children.Add(dateText);
        ////        fixedPage.Children.Add(PageNumberText);

        ////        // calc Current Page Height to know the rest Height of this page
        ////        CurrentPageHeight += gridToAdd.Items.Count * RowHeight;

        ////        // all grid rows added
        ////        if (lastRowIndex >= grid.Items.Count)
        ////        {
        ////            // if footer have space it will be added to the same page
        ////            if (footerHeight < (PageHeightWithMargin - CurrentPageHeight))
        ////            {
        ////                FixedPage.SetTop(footer, CurrentPageHeight + Margin);
        ////                FixedPage.SetLeft(footer, Margin);

        ////                fixedPage.Children.Add(footer);
        ////                IsFooterAdded = true;
        ////            }
        ////        }

        ////        fixedPage.Measure(pageSize);
        ////        fixedPage.Arrange(new Rect(new Point(), pageSize));
        ////        fixedPage.UpdateLayout();

        ////        PageContent pageContent = new PageContent();
        ////        ((IAddChild)pageContent).AddChild(fixedPage);
        ////        fixedDoc.Pages.Add(pageContent);

        ////        pageNumber++;
        ////        // go to start position : New page Top
        ////        CurrentPageHeight = Margin;

        ////        // this mean that lastRowIndex >= grid.Items.Count  and the footer dont have enough space
        ////        if (lastRowIndex >= grid.Items.Count && !IsFooterAdded)
        ////        {
        ////            FixedPage ffixedPage = new FixedPage();
        ////            ffixedPage.Background = Brushes.White;
        ////            ffixedPage.Width = pageSize.Width;
        ////            ffixedPage.Height = pageSize.Height;

        ////            FixedPage.SetTop(footer, Margin);
        ////            FixedPage.SetLeft(footer, Margin);

        ////            TextBlock fdateText = new TextBlock();
        ////            if (DateVisibility) fdateText.Visibility = Visibility.Visible;
        ////            else fdateText.Visibility = Visibility.Hidden;
        ////            dateText.Text = Now;

        ////            TextBlock fPageNumberText = new TextBlock();
        ////            if (PageNumberVisibility) fPageNumberText.Visibility = Visibility.Visible;
        ////            else fPageNumberText.Visibility = Visibility.Hidden;
        ////            fPageNumberText.Text = "Page : " + pageNumber;

        ////            FixedPage.SetTop(fdateText, PageHeightWithMargin);
        ////            FixedPage.SetLeft(fdateText, Margin);

        ////            FixedPage.SetTop(fPageNumberText, PageHeightWithMargin);
        ////            FixedPage.SetLeft(fPageNumberText, PageWidthWithMargin - PageNumberText.ActualWidth);

        ////            ffixedPage.Children.Add(footer);
        ////            ffixedPage.Children.Add(fdateText);
        ////            ffixedPage.Children.Add(fPageNumberText);

        ////            ffixedPage.Measure(pageSize);
        ////            ffixedPage.Arrange(new Rect(new Point(), pageSize));
        ////            ffixedPage.UpdateLayout();

        ////            PageContent fpageContent = new PageContent();
        ////            ((IAddChild)fpageContent).AddChild(ffixedPage);
        ////            fixedDoc.Pages.Add(fpageContent);
        ////            IsFooterAdded = true;
        ////        }

        ////        if (IsFooterAdded)
        ////        {
        ////            break;
        ////        }
        ////    }
        ////    PrintFixedDocument(fixedDoc, printDialog);
        ////}
        ////private DataGrid GetDataGrid(DataGrid grid, double GridActualWidth, double PageWidthWithMargin)
        ////{
        ////    DataGrid printed = new DataGrid();


        ////    // styling the grid
        ////    Style rowStyle = new Style(typeof(DataGridRow));
        ////    rowStyle.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.White));
        ////    rowStyle.Setters.Add(new Setter(Control.FontSizeProperty, FontSize));
        ////    if (IsBold) rowStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.Bold));
        ////    rowStyle.Setters.Add(new Setter(Control.HeightProperty, RowHeight));


        ////    Style columnStyle = new Style(typeof(DataGridColumnHeader));
        ////    columnStyle.Setters.Add(new Setter(Control.FontSizeProperty, HeaderFontSize));
        ////    columnStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
        ////    columnStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0, 0.5, 0, 1.5)));
        ////    columnStyle.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.White));
        ////    columnStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Black));
        ////    columnStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.SemiBold));


        ////    printed.RowStyle = rowStyle;

        ////    printed.VerticalGridLinesBrush = Brushes.Black;
        ////    printed.HorizontalGridLinesBrush = Brushes.Black;
        ////    printed.FontFamily = new FontFamily("Arial");

        ////    printed.RowBackground = Brushes.White;
        ////    printed.Background = Brushes.White;
        ////    printed.Foreground = Brushes.Black;
        ////    // get the columns of grid 
        ////    foreach (var column in grid.Columns)
        ////    {
        ////        if (column.Visibility != Visibility.Visible) continue;
        ////        DataGridTextColumn textColumn = new DataGridTextColumn();
        ////        textColumn.HeaderStyle = columnStyle;
        ////        textColumn.Header = column.Header;
        ////        textColumn.Width = column.ActualWidth / GridActualWidth * PageWidthWithMargin;
        ////        textColumn.Binding = ((DataGridTextColumn)column).Binding;
        ////        printed.Columns.Add(textColumn);
        ////    }
        ////    printed.BorderBrush = Brushes.Black;


        ////    return printed;
        ////}
        ////public void PrintFixedDocument(FixedDocument fixedDoc, PrintDialog printDialog)
        ////{
        ////    XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
        ////    writer.Write(fixedDoc, printDialog.PrintTicket); 
        ////}

    }
}
