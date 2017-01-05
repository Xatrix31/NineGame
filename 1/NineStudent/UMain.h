//---------------------------------------------------------------------------

#ifndef UMainH
#define UMainH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Menus.hpp>
#include <ExtCtrls.hpp>
#include <Graphics.hpp>


#include "UNamePlayer.h"
#include <IniFiles.hpp>
#include <ImgList.hpp>
#include "UCard.h"
#include "global.h"


//---------------------------------------------------------------------------
class TMainForm : public TForm
{
__published:	// IDE-managed Components
  TMainMenu *MainMenu;
  TMenuItem *N1;
  TMenuItem *NNewGame;
  TMenuItem *NNewPlayer;
  TMenuItem *NExit;
  TBevel *Bevel1;
  TImage *ImBank;
  TLabel *LMoneyBank;
  TLabel *LNameUser;
  TLabel *LNameLeft;
  TLabel *LNameRight;
  TImageList *ImageList1;
  TImage *ImMoneyLeft;
  TImage *ImMoneyRight;
  TImage *ImMoneyUser;
  TLabel *LMoneyLeft;
  TLabel *LMoneyRight;
  TLabel *LMoneyUser;
  TTimer *TimerMain;
  TMenuItem *NNextGame;
  TPopupMenu *PopupMenu;
  TMenuItem *NPopupNextgame;
  TMenuItem *NPopupNewGamr;
  TLabel *LTablo;
  TMenuItem *N61;
  TMenuItem *N2;
  TMenuItem *N3;
  TMenuItem *N4;
  TMenuItem *N5;
  void __fastcall FormPaint(TObject *Sender);
  void __fastcall NExitClick(TObject *Sender);
  void __fastcall NNewGameClick(TObject *Sender);
  void __fastcall NNewPlayerClick(TObject *Sender);
  void __fastcall TimerMainTimer(TObject *Sender);
  void __fastcall NNextGameClick(TObject *Sender);
private:
  bool m_bEndGame;
  bool m_bEndParty;
  void InitPack();
  void CardToPack(int x, int y);
  void DistrPack();
  void InitCoordinats();
  void InitMoney(bool reset);

  void MoneyToBank(int player);	// сбор денег в банк
public:		// User declarations
  TFCard* m_Card[36];
  TFNamePlayer* m_wndNamePlayer;
  enum mast{e_kresti=1, e_vini, e_chervi, e_bubi};
  enum {e_6=1, e_7, e_8, e_9, e_10, e_V, e_D, e_K, e_T} CardsValue;
  __fastcall TMainForm(TComponent* Owner);
  void RegistrationUser();
  int UserCardLeft[12];
  int CompCardTop[12];

  // указатель новая игра или нет
  bool m_IsNewParty;
  bool m_IsFirstMove;// указатель первого хода в игре

  // функция хода компьютерных игроков
  void GoComp();

  //функция хода пользователя
  bool GoUser(TFCard* card);

  //  кто ходит?
  void WhoGoes();

  // координаты карт на столе
  // значения масти и достоинства карты
  // определяет ее место на столе
  TPoint CoordinatesTableCard[4][9];

  // здесь будут хранится монетки игроков
  int m_UserMoney;
  int m_LeftMoney;
  int m_RightMoney;
  int m_BankMoney;


  bool IsProcat();
  bool CheckEndGame();
  bool DoGoCard(TFCard* card);
  void ShufflePack();
  bool IsEndGame();
  void MoneyToPlayer(int winner);
  void UserCardsPos();
  void DoProcat();
  bool CheckEndParty();
};
//---------------------------------------------------------------------------
extern PACKAGE TMainForm *MainForm;
//---------------------------------------------------------------------------
#endif
