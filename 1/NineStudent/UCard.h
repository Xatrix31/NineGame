//---------------------------------------------------------------------------
//        ����� ����� ��������������� ������� ��� ��� �������
//---------------------------------------------------------------------------


#ifndef UCardH
#define UCardH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
#include <Graphics.hpp>



//---------------------------------------------------------------------------
class TFCard : public TForm
{
__published:	// IDE-managed Components
  TImage *Face;// ���� �����
  TImage *Shirt;// ������� �����
  void __fastcall FormClick(TObject *Sender);
private:
 
public:
  int m_Mast;//  ����� �����
  int m_Value;// ����������� �����

  // ��������� ��������� ����� - � ������ ������ ��� �� ����� ���
  int m_State;
  
  __fastcall TFCard(TComponent* Owner);
  void __fastcall CardOpen(bool open);
  void InitFace(Graphics::TBitmap* bmp, int mast, int value);
};
//---------------------------------------------------------------------------
extern PACKAGE TFCard *FCard;
//---------------------------------------------------------------------------
#endif
