//---------------------------------------------------------------------------
//
//                     ������� �����
//             http://www.interestprograms.ru
//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "UNamePlayer.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFNamePlayer *FNamePlayer;
//---------------------------------------------------------------------------
__fastcall TFNamePlayer::TFNamePlayer(TComponent* Owner)
  : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFNamePlayer::ButtonOkClick(TObject *Sender)
{
  m_NamePlayer = EditName->Text;


  ModalResult  = mrOk;// ���������� ��������� OK
}
//---------------------------------------------------------------------------
void __fastcall TFNamePlayer::ButtonCancelClick(TObject *Sender)
{
  // ����� ����� �������� ��� ������������ � ������ ������� ������� ������
  m_NamePlayer = "���� ���";

  
  ModalResult  = mrCancel;// ���������� ��������� CANCEL
}
//---------------------------------------------------------------------------

