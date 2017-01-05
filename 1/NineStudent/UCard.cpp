//---------------------------------------------------------------------------
//
//                     ������� �����
//             http://www.interestprograms.ru
//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "UCard.h"
#include "UMain.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFCard *FCard;


//---------------------------------------------------------------------------
__fastcall TFCard::TFCard(TComponent* Owner)
  : TForm(Owner)
{
  this->Width  = Shirt->Width;
  this->Height = Shirt->Height;

  Face->Width  = Shirt->Width;
  Face->Height = Shirt->Height;
}
//---------------------------------------------------------------------------


// ���������, ��������� �����
void __fastcall TFCard::CardOpen(bool open)
{
  Face->Visible  = open;
  Shirt->Visible = !open;
}


//������������� ������� ������� ���� � ���������� ����������� ��������
void TFCard::InitFace(Graphics::TBitmap* bmp, int mast, int value)
{
  // ������ ����� ���� ����
  // ������� � ���� ���� ����������
  Face->Picture->Assign(bmp);
  m_Mast  = mast;
  m_Value = value;
}


void __fastcall TFCard::FormClick(TObject *Sender)
{
  // ���� ���� ��������� ������ �� ������ ���������
  if(MainForm->IsEndGame() == true) return;


  // �� ������ ������ ������
  // ���� ��� ����� ������������ � ��� ������������
  if(m_State == USERPLAYER && GOINGPLAYER == USERPLAYER){
    MainForm->GoUser(this);
  }
}
//---------------------------------------------------------------------------

