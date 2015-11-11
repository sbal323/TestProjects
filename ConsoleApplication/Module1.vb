Module Module1

    Sub Main()
        TestJoin()
        Console.ReadKey()
    End Sub

#Region "Tests"
    Private Sub TestJoin()
        Dim kpis As New List(Of DepartmentKpi)
        kpis.Add(New DepartmentKpi(1, 1, 25))
        kpis.Add(New DepartmentKpi(1, 1, 5))
        kpis.Add(New DepartmentKpi(1, 2, 25))
        kpis.Add(New DepartmentKpi(1, 2, 30))
        kpis.Add(New DepartmentKpi(Nothing, Nothing, 30))
        kpis.Add(New DepartmentKpi(2, 1, 25))
        kpis.Add(New DepartmentKpi(2, 1, 2))
        Dim kpisLib As New List(Of DepartmentKpiLib)
        kpisLib.Add(New DepartmentKpiLib(1, 1, 25))
        kpisLib.Add(New DepartmentKpiLib(2, 1, 50))
        'kpis.GroupJoin(kpisLib, Function(k) New With {Key .depID = k.DepartmentID, Key .kpiID = k.KpiID}, Function(l) New With {Key .depID = l.DepartmentID, Key .kpiID = l.KpiID}, Function(k, l) New With {k, l}).Select(Function(j) New DepartmentKpi(j.k.DepartmentID, j.k.KpiID, j.k.Value, If(j.l.FirstOrDefault IsNot Nothing, j.l.FirstOrDefault.Weight, -1))).ToList.ForEach(Sub(kp) Console.WriteLine(kp.ToString))
        kpis.Join(kpisLib, Function(k) New With {Key .depID = k.DepartmentID, Key .kpiID = k.KpiID}, Function(l) New With {Key .depID = l.DepartmentID, Key .kpiID = l.KpiID}, Function(k, l) New With {k, l}).Select(Function(j) New DepartmentKpi(j.k.DepartmentID, j.k.KpiID, j.k.Value, j.l.Weight)).ToList.ForEach(Sub(kp) Console.WriteLine(kp.ToString))
        'kpis.Join(kpisLib, Function(k) k.DepartmentID, Function(l) l.DepartmentID, Function(k, l) New With {k, l}).Select(Function(j) New DepartmentKpi(j.k.DepartmentID, j.k.KpiID, j.k.Value, j.l.Weight)).tolist.ForEach(Sub(kp) Console.WriteLine(kp.ToString))
    End Sub
    Private Sub TestGroupBy()
        Dim kpis As New List(Of DepartmentKpi)
        kpis.Add(New DepartmentKpi(1, 1, 25))
        kpis.Add(New DepartmentKpi(1, 1, 5))
        kpis.Add(New DepartmentKpi(1, 2, 25))
        kpis.Add(New DepartmentKpi(1, 2, 30))
        kpis.Add(New DepartmentKpi(2, 1, 25))
        kpis.Add(New DepartmentKpi(2, 1, 2))
        kpis.OrderByDescending(Function(x) x.Value).GroupBy(Function(dk) New With {Key .depID = dk.DepartmentID, Key .kpiID = dk.KpiID}).Select(Function(grp) grp.FirstOrDefault).ToList().ForEach(Sub(kp) Console.WriteLine(kp.ToString))
    End Sub
    Private Sub TestDate()
        Dim _startDate As Date = New Date(2016, 2, 12)
        Dim _endDate As Date = New Date(2015, 8, 12)
        Dim dates As List(Of Date) = GetMonths(_startDate, _endDate)
        For Each d In dates
            Console.WriteLine(d.ToString("dd MMM yyyy"))
        Next
    End Sub
#End Region

#Region "Aux"

    Private Function GetMonths(_startDate As Date, _endDate As Date) As List(Of Date)
        Dim months As New List(Of Date)
        If _startDate.Year = _endDate.Year Then
            For m As Integer = _startDate.Month To _endDate.Month
                months.Add(New Date(_startDate.Year, m, Date.DaysInMonth(_startDate.Year, m)))
            Next
        ElseIf _startDate.Year < _endDate.Year Then
            Dim initY As Integer = _startDate.Year
            Do
                If initY < _endDate.Year Then
                    months.AddRange(GetMonths(_startDate, New Date(_startDate.Year, 12, Date.DaysInMonth(_startDate.Year, 12))))
                Else
                    months.AddRange(GetMonths(New Date(_endDate.Year, 1, 1), _endDate))
                End If
                initY += 1
            Loop While initY <= _endDate.Year
        Else
            months.Add(New Date(_startDate.Year, _startDate.Month, Date.DaysInMonth(_startDate.Year, _startDate.Month)))
        End If
        Return months
    End Function

#End Region

#Region "Types"

    Class DepartmentKpi
        Public KpiID As Integer
        Public DepartmentID As Integer
        Public Value As Integer
        Public Weight As Integer
        Public Sub New(department As Integer, kpi As Integer, value As Integer)
            Me.DepartmentID = department
            Me.KpiID = kpi
            Me.Value = value
            Me.Weight = 0
        End Sub
        Public Sub New(department As Integer, kpi As Integer, value As Integer, weight As Integer)
            Me.DepartmentID = department
            Me.KpiID = kpi
            Me.Value = value
            Me.Weight = weight
        End Sub
        Public Overrides Function ToString() As String
            Return Me.DepartmentID & vbTab & Me.KpiID & vbTab & Me.Value & vbTab & Me.Weight
        End Function
    End Class

    Class DepartmentKpiLib
        Public KpiID As Integer
        Public DepartmentID As Integer
        Public Weight As Integer

        Public Sub New(department, kpi, weight)
            Me.DepartmentID = department
            Me.KpiID = kpi
            Me.Weight = weight
        End Sub
    End Class

#End Region

End Module
