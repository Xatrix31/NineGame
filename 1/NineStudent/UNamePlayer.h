//---------------------------------------------------------------------------

#ifndef UNamePlayerH
#define UNamePlayerH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
//---------------------------------------------------------------------------
class TFNamePlayer : public TForm
{
__published:	// IDE-managed Components
  TButton *ButtonOk;
  TEdit *EditName;
  TButton *ButtonCancel;
  void __fastcall ButtonOkClick(TObject *Sender);
  void __fastcall ButtonCancelClick(TObject *Sender);
private:	// User declarations
public:
  AnsiString m_NamePlayer;		// User declarations
  __fastcall TFNamePlayer(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFNamePlayer *FNamePlayer;
//---------------------------------------------------------------------------
#endif
