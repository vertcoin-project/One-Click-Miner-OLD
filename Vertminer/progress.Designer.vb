<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class progress
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(progress))
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.progress_text = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.SystemColors.Control
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
        Me.ProgressBar1.MaximumSize = New System.Drawing.Size(176, 47)
        Me.ProgressBar1.MinimumSize = New System.Drawing.Size(176, 47)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(176, 47)
        Me.ProgressBar1.TabIndex = 0
        '
        'progress_text
        '
        Me.progress_text.BackColor = System.Drawing.SystemColors.ControlLight
        Me.progress_text.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.progress_text.ForeColor = System.Drawing.SystemColors.WindowText
        Me.progress_text.Location = New System.Drawing.Point(12, 58)
        Me.progress_text.Name = "progress_text"
        Me.progress_text.ReadOnly = True
        Me.progress_text.Size = New System.Drawing.Size(176, 20)
        Me.progress_text.TabIndex = 0
        Me.progress_text.TabStop = False
        Me.progress_text.Text = "Progress Bar"
        Me.progress_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'progress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(200, 90)
        Me.Controls.Add(Me.progress_text)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(200, 70)
        Me.Name = "progress"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Downloading.."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents progress_text As TextBox
End Class
