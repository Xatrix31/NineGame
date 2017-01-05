//---------------------------------------------------------------------------
//
//                     КЛОЧКОВ ПАВЕЛ
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


  ModalResult  = mrOk;// возвращаем результат OK
}
//---------------------------------------------------------------------------
void __fastcall TFNamePlayer::ButtonCancelClick(TObject *Sender)
{
  // здесь можно изменить имя пользователя в случае нажатия клавиши Отмена
  m_NamePlayer = "Ваше имя";

  
  ModalResult  = mrCancel;// возвращаем результат CANCEL
}
//---------------------------------------------------------------------------

