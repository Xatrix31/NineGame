object FNamePlayer: TFNamePlayer
  Left = 356
  Top = 288
  BorderStyle = bsDialog
  Caption = #1042#1072#1096#1077' '#1080#1084#1103' '#1080#1075#1088#1086#1082'?'
  ClientHeight = 117
  ClientWidth = 300
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object ButtonOk: TButton
    Left = 64
    Top = 70
    Width = 75
    Height = 25
    Caption = #1044#1072
    TabOrder = 0
    OnClick = ButtonOkClick
  end
  object EditName: TEdit
    Left = 78
    Top = 24
    Width = 145
    Height = 21
    TabOrder = 1
    Text = #1045#1074#1075#1077#1096#1072
  end
  object ButtonCancel: TButton
    Left = 160
    Top = 70
    Width = 75
    Height = 25
    Caption = #1054#1090#1084#1077#1085#1072
    TabOrder = 2
    OnClick = ButtonCancelClick
  end
end
