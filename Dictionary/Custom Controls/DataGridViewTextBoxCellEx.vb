Imports System.Linq
Imports System.Windows.Forms
Imports System.Drawing

'DataGridViewTextBoxCellEx
'**********************************
'Thanks to codeproject for giving coders a place to share
'This was used from: http://www.codeproject.com/Articles/34037/DataGridVewTextBoxCell-with-Span-Behaviour


Namespace SpannedDataGridView
    Public Interface ISpannedCell
        ReadOnly Property ColumnSpan() As Integer
        ReadOnly Property RowSpan() As Integer
        ReadOnly Property OwnerCell() As DataGridViewCell
    End Interface
End Namespace

Namespace SpannedDataGridView
    Public Class DataGridViewTextBoxCellEx
        Inherits DataGridViewTextBoxCell
        Implements ISpannedCell
#Region "Fields"
        Private m_ColumnSpan As Integer = 1
        Private m_RowSpan As Integer = 1
        Private m_OwnerCell As DataGridViewTextBoxCellEx
#End Region

#Region "Properties"

        Public Property ColumnSpan() As Integer
            Get
                Return m_ColumnSpan
            End Get
            Set(ByVal value As Integer)
                If DataGridView Is Nothing OrElse m_OwnerCell IsNot Nothing Then
                    Return
                End If
                If value < 1 OrElse ColumnIndex + value - 1 >= DataGridView.ColumnCount Then
                    Throw New System.ArgumentOutOfRangeException("value")
                End If
                If m_ColumnSpan <> value Then
                    SetSpan(value, m_RowSpan)
                End If
            End Set
        End Property

        Public Property RowSpan() As Integer
            Get
                Return m_RowSpan
            End Get
            Set(ByVal value As Integer)
                If DataGridView Is Nothing OrElse m_OwnerCell IsNot Nothing Then
                    Return
                End If
                If value < 1 OrElse RowIndex + value - 1 >= DataGridView.RowCount Then
                    Throw New System.ArgumentOutOfRangeException("value")
                End If
                If m_RowSpan <> value Then
                    SetSpan(m_ColumnSpan, value)
                End If
            End Set
        End Property

        Public Property OwnerCell() As DataGridViewCell
            Get
                Return m_OwnerCell
            End Get
            Private Set(ByVal value As DataGridViewCell)
                m_OwnerCell = TryCast(value, DataGridViewTextBoxCellEx)
            End Set
        End Property

        Public Overrides Property [ReadOnly]() As Boolean
            Get
                Return MyBase.[ReadOnly]
            End Get
            Set(ByVal value As Boolean)
                MyBase.[ReadOnly] = value

                If m_OwnerCell Is Nothing AndAlso (m_ColumnSpan > 1 OrElse m_RowSpan > 1) AndAlso DataGridView IsNot Nothing Then
                    For Each col In Enumerable.Range(ColumnIndex, m_ColumnSpan)
                        For Each row In Enumerable.Range(RowIndex, m_RowSpan)
                            If col <> ColumnIndex OrElse row <> RowIndex Then
                                DataGridView(col, row).[ReadOnly] = value
                            End If
                        Next
                    Next
                End If
            End Set
        End Property

#End Region

#Region "Painting."

        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, ByVal value As Object, _
         ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
            If m_OwnerCell IsNot Nothing AndAlso m_OwnerCell.DataGridView Is Nothing Then
                m_OwnerCell = Nothing
            End If
            'owner cell was removed.
            If DataGridView Is Nothing OrElse (m_OwnerCell Is Nothing AndAlso m_ColumnSpan = 1 AndAlso m_RowSpan = 1) Then
                MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                 formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
                Return
            End If

            Dim ownerCell = Me
            Dim columnIndex__1 = ColumnIndex
            Dim columnSpan = m_ColumnSpan
            Dim rowSpan = m_RowSpan
            If m_OwnerCell IsNot Nothing Then
                ownerCell = m_OwnerCell
                columnIndex__1 = m_OwnerCell.ColumnIndex
                rowIndex = m_OwnerCell.RowIndex
                columnSpan = m_OwnerCell.ColumnSpan
                rowSpan = m_OwnerCell.RowSpan
                value = m_OwnerCell.GetValue(rowIndex)
                errorText = m_OwnerCell.GetErrorText(rowIndex)
                cellState = m_OwnerCell.State
                cellStyle = m_OwnerCell.GetInheritedStyle(Nothing, rowIndex, True)
                formattedValue = m_OwnerCell.GetFormattedValue(value, rowIndex, cellStyle, Nothing, Nothing, DataGridViewDataErrorContexts.Display)
            End If
            If CellsRegionContainsSelectedCell(columnIndex__1, rowIndex, columnSpan, rowSpan) Then
                'I hated the blue highlight on each cell prefering to set my own color
                'cellState = cellState Or DataGridViewElementStates.Selected
                cellState = cellState Or DataGridViewElementStates.Visible
            End If
            Dim cellBounds2 = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(Me, cellBounds, DataGridView.SingleVerticalBorderAdded(), DataGridView.SingleHorizontalBorderAdded())
            clipBounds = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds2, DataGridView.SingleVerticalBorderAdded(), DataGridView.SingleHorizontalBorderAdded())
            Using g = DataGridView.CreateGraphics()
                g.SetClip(clipBounds)
                'Paint the content.
                advancedBorderStyle = DataGridViewCellExHelper.AdjustCellBorderStyle(ownerCell)
                ownerCell.NativePaint(g, clipBounds, cellBounds2, rowIndex, cellState, value, _
                 formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts And Not DataGridViewPaintParts.Border)
                'Paint the borders.
                If (paintParts And DataGridViewPaintParts.Border) <> DataGridViewPaintParts.None Then
                    Dim leftTopCell = ownerCell
                    Dim advancedBorderStyle2 = New DataGridViewAdvancedBorderStyle() With { _
                      .Left = advancedBorderStyle.Left, _
                      .Top = advancedBorderStyle.Top, _
                      .Right = DataGridViewAdvancedCellBorderStyle.None, _
                      .Bottom = DataGridViewAdvancedCellBorderStyle.None _
                    }
                    leftTopCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle2)

                    Dim rightBottomCell = If(TryCast(DataGridView(columnIndex__1 + columnSpan - 1, rowIndex + rowSpan - 1), DataGridViewTextBoxCellEx), Me)
                    Dim advancedBorderStyle3 = New DataGridViewAdvancedBorderStyle() With { _
                        .Left = DataGridViewAdvancedCellBorderStyle.None, _
                        .Top = DataGridViewAdvancedCellBorderStyle.None, _
                        .Right = advancedBorderStyle.Right, _
                        .Bottom = advancedBorderStyle.Bottom _
                    }
                    rightBottomCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle3)
                End If
            End Using
        End Sub

        Private Sub NativePaint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, ByVal value As Object, _
         ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
             formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
        End Sub

#End Region

#Region "Spanning."

        Private Sub SetSpan(ByVal columnSpan As Integer, ByVal rowSpan As Integer)
            Dim prevColumnSpan As Integer = m_ColumnSpan
            Dim prevRowSpan As Integer = m_RowSpan
            m_ColumnSpan = columnSpan
            m_RowSpan = rowSpan

            If DataGridView IsNot Nothing Then
                ' clear.
                For Each rowIndex__1 As Integer In Enumerable.Range(RowIndex, prevRowSpan)
                    For Each columnIndex__2 As Integer In Enumerable.Range(ColumnIndex, prevColumnSpan)
                        Dim cell = TryCast(DataGridView(columnIndex__2, rowIndex__1), DataGridViewTextBoxCellEx)
                        If cell IsNot Nothing Then
                            cell.OwnerCell = Nothing
                        End If
                    Next
                Next

                ' set.
                For Each rowIndex__1 As Integer In Enumerable.Range(RowIndex, m_RowSpan)
                    For Each columnIndex__2 As Integer In Enumerable.Range(ColumnIndex, m_ColumnSpan)
                        Dim cell = TryCast(DataGridView(columnIndex__2, rowIndex__1), DataGridViewTextBoxCellEx)
                        If cell IsNot Nothing AndAlso cell IsNot Me Then
                            If cell.ColumnSpan > 1 Then
                                cell.ColumnSpan = 1
                            End If
                            If cell.RowSpan > 1 Then
                                cell.RowSpan = 1
                            End If
                            cell.OwnerCell = Me
                        End If
                    Next
                Next

                OwnerCell = Nothing
                DataGridView.Invalidate()
            End If
        End Sub

#End Region

#Region "Editing."

        Public Overrides Function PositionEditingPanel(ByVal cellBounds As Rectangle, ByVal cellClip As Rectangle, ByVal cellStyle As DataGridViewCellStyle, ByVal singleVerticalBorderAdded As Boolean, ByVal singleHorizontalBorderAdded As Boolean, ByVal isFirstDisplayedColumn As Boolean, _
         ByVal isFirstDisplayedRow As Boolean) As Rectangle
            If m_OwnerCell Is Nothing AndAlso m_ColumnSpan = 1 AndAlso m_RowSpan = 1 Then
                Return MyBase.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, _
                 isFirstDisplayedRow)
            End If

            Dim ownerCell = Me
            If m_OwnerCell IsNot Nothing Then
                Dim rowIndex = m_OwnerCell.RowIndex
                cellStyle = m_OwnerCell.GetInheritedStyle(Nothing, rowIndex, True)
                m_OwnerCell.GetFormattedValue(m_OwnerCell.Value, rowIndex, cellStyle, Nothing, Nothing, DataGridViewDataErrorContexts.Formatting)
                Dim editingControl = TryCast(DataGridView.EditingControl, IDataGridViewEditingControl)
                If editingControl IsNot Nothing Then
                    editingControl.ApplyCellStyleToEditingControl(cellStyle)
                    Dim editingPanel = DataGridView.EditingControl.Parent
                    If editingPanel IsNot Nothing Then
                        editingPanel.BackColor = cellStyle.BackColor
                    End If
                End If
                ownerCell = m_OwnerCell
            End If
            cellBounds = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(Me, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded)
            cellClip = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded)
            Return MyBase.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, ownerCell.InFirstDisplayedColumn(), _
             ownerCell.InFirstDisplayedRow())
        End Function

        Protected Overrides Function GetValue(ByVal rowIndex As Integer) As Object
            If m_OwnerCell IsNot Nothing Then
                Return m_OwnerCell.GetValue(m_OwnerCell.RowIndex)
            End If
            Return MyBase.GetValue(rowIndex)
        End Function

        Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
            If m_OwnerCell IsNot Nothing Then
                Return m_OwnerCell.SetValue(m_OwnerCell.RowIndex, value)
            End If
            Return MyBase.SetValue(rowIndex, value)
        End Function

#End Region

#Region "Other overridden"

        Protected Overrides Sub OnDataGridViewChanged()
            MyBase.OnDataGridViewChanged()

            If DataGridView Is Nothing Then
                m_ColumnSpan = 1
                m_RowSpan = 1
            End If
        End Sub

        Protected Overrides Function BorderWidths(ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle) As Rectangle
            If m_OwnerCell Is Nothing AndAlso m_ColumnSpan = 1 AndAlso m_RowSpan = 1 Then
                Return MyBase.BorderWidths(advancedBorderStyle)
            End If

            If m_OwnerCell IsNot Nothing Then
                Return m_OwnerCell.BorderWidths(advancedBorderStyle)
            End If

            Dim leftTop = MyBase.BorderWidths(advancedBorderStyle)
            Dim rightBottomCell = TryCast(DataGridView(ColumnIndex + ColumnSpan - 1, RowIndex + RowSpan - 1), DataGridViewTextBoxCellEx)
            Dim rightBottom = If(rightBottomCell IsNot Nothing, rightBottomCell.NativeBorderWidths(advancedBorderStyle), leftTop)
            Return New Rectangle(leftTop.X, leftTop.Y, rightBottom.Width, rightBottom.Height)
        End Function

        Private Function NativeBorderWidths(ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle) As Rectangle
            Return MyBase.BorderWidths(advancedBorderStyle)
        End Function

        Protected Overrides Function GetPreferredSize(ByVal graphics As Graphics, ByVal cellStyle As DataGridViewCellStyle, ByVal rowIndex__1 As Integer, ByVal constraintSize As Size) As Size
            If OwnerCell IsNot Nothing Then
                Return New Size(0, 0)
            End If
            Dim size = MyBase.GetPreferredSize(graphics, cellStyle, rowIndex__1, constraintSize)
            Dim grid = DataGridView
            Dim width = size.Width - Enumerable.Range(ColumnIndex + 1, ColumnSpan - 1).[Select](Function(index) grid.Columns(index).Width).Sum()
            Dim height = size.Height - Enumerable.Range(RowIndex + 1, RowSpan - 1).[Select](Function(index) grid.Rows(index).Height).Sum()
            Return New Size(width, height)
        End Function

#End Region

#Region "Private Methods"

        Private Function CellsRegionContainsSelectedCell(ByVal columnIndex As Integer, ByVal rowIndex As Integer, ByVal columnSpan As Integer, ByVal rowSpan As Integer) As Boolean
            If DataGridView Is Nothing Then
                Return False
            End If

            Return (From col In Enumerable.Range(columnIndex, columnSpan) From row In Enumerable.Range(rowIndex, rowSpan) Where DataGridView(col, row).ColumnIndex).Any()
        End Function

#End Region

        Public ReadOnly Property ColumnSpan1 As Integer Implements ISpannedCell.ColumnSpan
            Get
                Return ColumnSpan
            End Get
        End Property

        Public ReadOnly Property OwnerCell1 As System.Windows.Forms.DataGridViewCell Implements ISpannedCell.OwnerCell
            Get
                Return OwnerCell
            End Get
        End Property

        Public ReadOnly Property RowSpan1 As Integer Implements ISpannedCell.RowSpan
            Get
                Return RowSpan
            End Get
        End Property
    End Class

    Public Class DataGridViewTextBoxColumnEx
        Inherits DataGridViewColumn
#Region "ctor"
        Public Sub New()
            MyBase.New(New DataGridViewTextBoxCellEx())
        End Sub
#End Region
    End Class
End Namespace

Namespace SpannedDataGridView
    NotInheritable Class DataGridViewCellExHelper
        Private Sub New()
        End Sub
        Public Shared Function GetSpannedCellClipBounds(Of TCell As {DataGridViewCell, ISpannedCell})(ByVal ownerCell As TCell, ByVal cellBounds As Rectangle, ByVal singleVerticalBorderAdded As Boolean, ByVal singleHorizontalBorderAdded As Boolean) As Rectangle
            Dim dataGridView = ownerCell.DataGridView
            Dim clipBounds = cellBounds
            'Setting X (skip invisible columns).
            For Each columnIndex In Enumerable.Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan)
                Dim column = dataGridView.Columns(columnIndex)
                If Not column.Visible Then
                    Continue For
                End If
                If column.Frozen OrElse columnIndex > dataGridView.FirstDisplayedScrollingColumnIndex Then
                    Exit For
                End If
                If columnIndex = dataGridView.FirstDisplayedScrollingColumnIndex Then
                    clipBounds.Width -= dataGridView.FirstDisplayedScrollingColumnHiddenWidth
                    If dataGridView.RightToLeft <> RightToLeft.Yes Then
                        clipBounds.X += dataGridView.FirstDisplayedScrollingColumnHiddenWidth
                    End If
                    Exit For
                End If
                clipBounds.Width -= column.Width
                If dataGridView.RightToLeft <> RightToLeft.Yes Then
                    clipBounds.X += column.Width
                End If
            Next

            'Setting Y.
            For Each rowIndex In Enumerable.Range(ownerCell.RowIndex, ownerCell.RowSpan)
                Dim row = dataGridView.Rows(rowIndex)
                If Not row.Visible Then
                    Continue For
                End If
                If row.Frozen OrElse rowIndex >= dataGridView.FirstDisplayedScrollingRowIndex Then
                    Exit For
                End If
                clipBounds.Y += row.Height
                clipBounds.Height -= row.Height
            Next

            ' exclude borders.
            If dataGridView.BorderStyle <> BorderStyle.None Then
                Dim clientRectangle = dataGridView.ClientRectangle
                clientRectangle.Width -= 1
                clientRectangle.Height -= 1
                If dataGridView.RightToLeft = RightToLeft.Yes Then
                    clientRectangle.X += 1
                    clientRectangle.Y += 1
                End If
                clipBounds.Intersect(clientRectangle)
            End If
            Return clipBounds
        End Function

        Public Shared Function GetSpannedCellBoundsFromChildCellBounds(Of TCell As {DataGridViewCell, ISpannedCell})(ByVal childCell As TCell, ByVal childCellBounds As Rectangle, ByVal singleVerticalBorderAdded As Boolean, ByVal singleHorizontalBorderAdded As Boolean) As Rectangle
            Dim dataGridView = childCell.DataGridView
            Dim ownerCell = If(TryCast(childCell.OwnerCell, TCell), childCell)
            Dim spannedCellBounds = childCellBounds
            '
            Dim firstVisibleColumnIndex = Enumerable.Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan).First(Function(i) dataGridView.Columns(i).Visible)
            If dataGridView.Columns(firstVisibleColumnIndex).Frozen Then
                spannedCellBounds.X = dataGridView.GetColumnDisplayRectangle(firstVisibleColumnIndex, False).X
            Else
                Dim dx = Enumerable.Range(firstVisibleColumnIndex, childCell.ColumnIndex - firstVisibleColumnIndex).[Select](Function(i) dataGridView.Columns(i)).Where(Function(columnItem) columnItem.Visible).Sum(Function(columnItem) columnItem.Width)
                spannedCellBounds.X = If(dataGridView.RightToLeft = RightToLeft.Yes, spannedCellBounds.X + dx, spannedCellBounds.X - dx)
            End If
            '
            Dim firstVisibleRowIndex = Enumerable.Range(ownerCell.RowIndex, ownerCell.RowSpan).First(Function(i) dataGridView.Rows(i).Visible)
            If dataGridView.Rows(firstVisibleRowIndex).Frozen Then
                spannedCellBounds.Y = dataGridView.GetRowDisplayRectangle(firstVisibleRowIndex, False).Y
            Else
                spannedCellBounds.Y -= Enumerable.Range(firstVisibleRowIndex, childCell.RowIndex - firstVisibleRowIndex).[Select](Function(i) dataGridView.Rows(i)).Where(Function(rowItem) rowItem.Visible).Sum(Function(rowItem) rowItem.Height)
            End If
            '
            Dim spannedCellWidth = Enumerable.Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan).[Select](Function(columnIndex) dataGridView.Columns(columnIndex)).Where(Function(column) column.Visible).Sum(Function(column) column.Width)
            If dataGridView.RightToLeft = RightToLeft.Yes Then
                spannedCellBounds.X = spannedCellBounds.Right - spannedCellWidth
            End If
            spannedCellBounds.Width = spannedCellWidth
            '
            spannedCellBounds.Height = Enumerable.Range(ownerCell.RowIndex, ownerCell.RowSpan).[Select](Function(rowIndex) dataGridView.Rows(rowIndex)).Where(Function(row) row.Visible).Sum(Function(row) row.Height)

            If singleVerticalBorderAdded AndAlso InFirstDisplayedColumn(ownerCell) Then
                spannedCellBounds.Width += 1
                If dataGridView.RightToLeft <> RightToLeft.Yes Then
                    If childCell.ColumnIndex <> dataGridView.FirstDisplayedScrollingColumnIndex Then
                        spannedCellBounds.X -= 1
                    End If
                Else
                    If childCell.ColumnIndex = dataGridView.FirstDisplayedScrollingColumnIndex Then
                        spannedCellBounds.X -= 1
                    End If
                End If
            End If
            If singleHorizontalBorderAdded AndAlso InFirstDisplayedRow(ownerCell) Then
                spannedCellBounds.Height += 1
                If childCell.RowIndex <> dataGridView.FirstDisplayedScrollingRowIndex Then
                    spannedCellBounds.Y -= 1
                End If
            End If
            Return spannedCellBounds
        End Function

        Public Shared Function AdjustCellBorderStyle(Of TCell As {DataGridViewCell, ISpannedCell})(ByVal cell As TCell) As DataGridViewAdvancedBorderStyle
            Dim dataGridViewAdvancedBorderStylePlaceholder = New DataGridViewAdvancedBorderStyle()
            Dim dataGridView = cell.DataGridView
            Return cell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, dataGridView.SingleVerticalBorderAdded(), dataGridView.SingleHorizontalBorderAdded(), InFirstDisplayedColumn(cell), InFirstDisplayedRow(cell))
        End Function
    End Class

    Module DataGridViewCellHelper
        <System.Runtime.CompilerServices.Extension()> _
        Public Function InFirstDisplayedColumn(Of TCell As {DataGridViewCell, ISpannedCell})(ByVal cell As TCell) As Boolean
            Dim dataGridView = cell.DataGridView
            Return dataGridView.FirstDisplayedScrollingColumnIndex >= cell.ColumnIndex AndAlso dataGridView.FirstDisplayedScrollingColumnIndex < cell.ColumnIndex + cell.ColumnSpan
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function InFirstDisplayedRow(Of TCell As {DataGridViewCell, ISpannedCell})(ByVal cell As TCell) As Boolean
            Dim dataGridView = cell.DataGridView
            Return dataGridView.FirstDisplayedScrollingRowIndex >= cell.RowIndex AndAlso dataGridView.FirstDisplayedScrollingRowIndex < cell.RowIndex + cell.RowSpan
        End Function
    End Module
End Namespace

Namespace SpannedDataGridView
    Module DataGridViewHelper
        <System.Runtime.CompilerServices.Extension()> _
        Public Function SingleHorizontalBorderAdded(ByVal dataGridView As DataGridView) As Boolean
            Return Not dataGridView.ColumnHeadersVisible AndAlso (dataGridView.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.[Single] OrElse dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal)
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function SingleVerticalBorderAdded(ByVal dataGridView As DataGridView) As Boolean
            Return Not dataGridView.RowHeadersVisible AndAlso (dataGridView.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.[Single] OrElse dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical)
        End Function
    End Module
End Namespace
