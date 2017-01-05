//---------------------------------------------------------------------------
//
//                     ������� �����
//             http://www.interestprograms.ru
//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "UMain.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TMainForm *MainForm;
//---------------------------------------------------------------------------
__fastcall TMainForm::TMainForm(TComponent* Owner)
  : TForm(Owner)
{
  //�������������
  randomize();// ������� ����� ��� ����������� ������������� ������ ShufflePack()
  m_IsNewParty   = true;// ��� ����� ������
  m_IsFirstMove  = true;// ������ ��� � ����

  TimerMain->Interval = 600;// ����� ����� �������� ����� �������� �������


  //����� ������� ��������� ���������� �������
  Bevel1->Align = alClient;

  InitPack();// ������� ������
  InitMoney(true);// ���� �� 100 ������

  // ��� ������������
  LNameUser->Caption = "������";//����� ������ �������� ��������� ��� � ���� �����������
  RegistrationUser();// �������� ����������� ����� �������� �����


}


/////////////////////////////////////////////////////////////////////////////
//---------------------------------------------------------------------------
//                          ������� �������������
//---------------------------------------------------------------------------
//         ����������� ��� ������� ���������� ��� ��� ������������� ����� ����

// ������������� ����� � ����
void TMainForm::InitMoney(bool reset)
{
  if(reset == true){
    //���� �� 100 ��������
    m_UserMoney  = 100; //  ���
    m_LeftMoney  = 100; //  ���
    m_RightMoney = 100; //  ���
    m_BankMoney  = 0;   //  ���
  }

  LMoneyUser->Caption  = IntToStr(m_UserMoney)  + "�.";
  LMoneyLeft->Caption  = IntToStr(m_LeftMoney)  + "�.";
  LMoneyRight->Caption = IntToStr(m_RightMoney) + "�.";
  LMoneyBank->Caption  = IntToStr(m_BankMoney)  + "�.";
}

void TMainForm::RegistrationUser()
{
  // ������� ���� ������� ��� ������������
  m_wndNamePlayer = new TFNamePlayer(this);

  m_wndNamePlayer->EditName->Text = LNameUser->Caption;

  //����� ������ ���������
  m_wndNamePlayer->ShowModal();
  
  // ������� ��������� ��� ������ ��� ��� � ������ ��������� ����� ������ �����
  LNameUser->Caption = m_wndNamePlayer->m_NamePlayer;

  //������� �� ������ ������ �� �������������
  // ����� ��������� �������� ��������� ����� ����������� ������ ������
  delete m_wndNamePlayer;
}


// ������������� ���� � �������� ������
void TMainForm::InitPack()
{
  Graphics::TBitmap* bmp = new Graphics::TBitmap;// ������� ������ bmp � ������������ ������
  int mast  = 0;
  int value = 0;
  for(int i = 0; i < 36; i++){
    m_Card[i] = new TFCard(MainForm/*this*/);
    m_Card[i]->Parent  = this;// ����� ����� ������ ������� �����
    m_Card[i]->Visible = true;// � ����� ������


    ImageList1->GetBitmap(i, bmp);// ��������� �������� �� ImageList
    // ����� ������ 9 ���� ������ ����� � �������� ������� � 6-��
    if( i % 9 == 0){ 
      mast++;
      value = 1;
    }

    // ���������� ������� �������, �����, ��������
    m_Card[i]->InitFace(bmp, mast, value);

    value++;
  }

  // ����� ����������� ������ ������� ������ �� ������
  // ����� ��� ������ �������� ��������� ����� ����������� ������ ������
  delete bmp;

  //������ �� �����
  CardToPack(Width/2-40, 10);

}



// ������������� ��������� ���������� ���� ������� �� �����
void TMainForm::InitCoordinats()
{
  int centerWidth = ClientWidth/2 - CARDWIDTH/2;
  UserCardLeft[0]  = centerWidth - 187;
  UserCardLeft[1]  = centerWidth - 153;
  UserCardLeft[2]  = centerWidth - 119;
  UserCardLeft[3]  = centerWidth - 85;
  UserCardLeft[4]  = centerWidth - 51;
  UserCardLeft[5]  = centerWidth - 17; // 34/2
  //------- �������� ----------------
  UserCardLeft[6]  = centerWidth + 17; // 34/2
  UserCardLeft[7]  = centerWidth + 51;
  UserCardLeft[8]  = centerWidth + 85;
  UserCardLeft[9]  = centerWidth + 119;
  UserCardLeft[10] = centerWidth + 153;
  UserCardLeft[11] = centerWidth + 187;
  

  int centerHeight = ClientHeight/2 - CARDHEIGHT/2;
  CompCardTop[0]  = centerHeight - 110;
  CompCardTop[1]  = centerHeight - 90;
  CompCardTop[2]  = centerHeight - 70;
  CompCardTop[3]  = centerHeight - 50;
  CompCardTop[4]  = centerHeight - 30;
  CompCardTop[5]  = centerHeight - 10; // 20/2
  //------ �������� ------------------
  CompCardTop[6]  = centerHeight + 10; // 20/2
  CompCardTop[7]  = centerHeight + 30;
  CompCardTop[8]  = centerHeight + 50;
  CompCardTop[9]  = centerHeight + 70;
  CompCardTop[10] = centerHeight + 90;
  CompCardTop[11] = centerHeight + 110;
}


//---------------------------------------------------------------------------

void __fastcall TMainForm::FormPaint(TObject *Sender)
{

  Canvas->Brush->Color = (TColor)RGB(200,200,200);
  //�������� ��������
  int width  = CARDWIDTH  + 2/* ��������� 2 ������� ��� �������*/;// ������ ������
  int height = CARDHEIGHT + 2;// ������ ������
  int x = CARDWIDTH  + /*���������� ����� ��������*/4;
  int y = CARDHEIGHT + /*���������� ����� ��������*/4;

  // ��������� ����� �� ������ ��� ���� �� ��������
  int firstX = ClientWidth/2  - (width*9)/2  - (8*2)/2; // ������ ����-������ �����������-�������� �� �������

  // � �� ������ ���� (�� 10 ��������) ������
  int firstY = ClientHeight/2 - (height*4)/2 - 10;

  for(int i = 0; i < 4; i++){
    for(int j = 0; j < 9; j++){

      Canvas->Rectangle(firstX +  x*j, firstY + y*i,
                      firstX + x*j+width, firstY + y*i+height);

      // ��� �� ��������� ���������� ���� �������� �����
      // ����� ����� ������������� ����� �� �������
      CoordinatesTableCard[i][j].x = firstX +  x*j + 1;
      CoordinatesTableCard[i][j].y = firstY +  y*i + 1;
    }
  }

}
//---------------------------------------------------------------------------


void __fastcall TMainForm::NExitClick(TObject *Sender)
{
  Close();// �������� ���������
}
//---------------------------------------------------------------------------


// ������� ����� � ���������� ������
void TMainForm::CardToPack(int x, int y)
{
  //����������� ������ � ������ �����
  int X = x; // ���������� ���������� ������ �� ������
  int Y = y; // ���������� ���������� ������ �� ������
  for(int i = 0; i < 36; i++){
      m_Card[i]->Left = X + i*0.3;
      m_Card[i]->Top  = Y + i*0.3;
  }
}


// ������� ����� ���� �������
void TMainForm::DistrPack()
{
  // ������������� ��������� ���� �������
  InitCoordinats();

  // ������� ��� �����
  for(int i = 0; i< 36; i++){
    m_Card[i]->CardOpen(false);
  }

  // ������ ��� ������� ����� ���������� ������
  ShufflePack();

  // ����� ����� ������ �� ����� � �����
  for(int player = 1; player <= 3; player++){
    MoneyToBank(player);
  } 


  int count = 0;
  // � ���� �������� ����� ����
  int countPlayer  = GOINGPLAYER;
  int countCardPos = 0;
  while(count != 36){
    switch(countPlayer){
      case USERPLAYER:
        m_Card[count]->Left = UserCardLeft[countCardPos];
        m_Card[count]->Top  = ClientHeight - CARDHEIGHT - 5;
        m_Card[count]->CardOpen(true);
        m_Card[count]->m_State = USERPLAYER;
        break;
      case LEFTPLAYER:
        m_Card[count]->Top  = CompCardTop[countCardPos];
        m_Card[count]->Left = 5;
        m_Card[count]->m_State = LEFTPLAYER;
        m_Card[count]->CardOpen(false);// ����� ������ ���������(false) � ���������(true) ����� ������ ������
        break;
      case RIGHTPLAYER:
        m_Card[count]->Top  = CompCardTop[countCardPos];
        m_Card[count]->Left = ClientWidth - CARDWIDTH - 5;
        m_Card[count]->m_State = RIGHTPLAYER;
        m_Card[count]->CardOpen(false);// ����� ������ ���������(false) � ���������(true) ����� ������� ������
        break;

    }
    countPlayer++;
    if(countPlayer == 4) countPlayer = 1;
    
    if( (count+1) % 3 == 0)   countCardPos++;

    count++;
  }


  // ��������� ����������� ����� ������������
  UserCardsPos();

  m_bEndGame = false;// ���������� �� ����������� ����
  //����� ������ ��� ������� ������������� ������� ���� ��� �� ����
  m_IsFirstMove = true;

  // ��������� ������� ������
  TimerMain->Enabled = true;
}


void __fastcall TMainForm::NNewGameClick(TObject *Sender)
{

  m_IsNewParty  = true;// ���������, ��� ��� ����� ������
  InitMoney(true);

  NNextGameClick(NULL);
}
//---------------------------------------------------------------------------

void __fastcall TMainForm::NNextGameClick(TObject *Sender)
{
  // ������ ����� ����, ������ ������ ������� ���� "��������� ����"
  // � ������ ����� ������ �.�. ��� ��������� ������ ����
  NNextGame->Enabled      = false;
  NPopupNextgame->Enabled = false;


  GOINGPLAYER++; // ����� ��������� ����� ���������� �����
  if(GOINGPLAYER == 4) GOINGPLAYER = 1;

  DistrPack();
}
//---------------------------------------------------------------------------



void __fastcall TMainForm::NNewPlayerClick(TObject *Sender)
{
  RegistrationUser();
}
//---------------------------------------------------------------------------

void __fastcall TMainForm::TimerMainTimer(TObject *Sender)
{
  //
  TimerMain->Enabled = false;

  // ���� ���� ���������, ���������� ���������� ���� ���������
  if(IsEndGame() == true) return;

  WhoGoes();

   
}
//---------------------------------------------------------------------------

// ������� ���� ������������ �������
void TMainForm::GoComp()
{
  //����� �������
  LTablo->Visible = false;

  // ���� ������ ����� ������ � ������ ��� � ����
  if(m_IsNewParty == true && m_IsFirstMove == true){
    //���� ������� ���� ���� �����
    for(int i = 0; i < 36; i++){
      if(m_Card[i]->m_State == GOINGPLAYER &&
         m_Card[i]->m_Value == e_9 && m_Card[i]->m_Mast == e_bubi){

         m_Card[i]->CardOpen(true);//��������� �����
         //��������������� ���
         DoGoCard(m_Card[i]);
         return;
      }
    }
  }
  else{


    bool BREAK = false;
    bool is_go = false;
    // ���� ����� ������� ����� ������ ������������ �����
    // ������� ���� �� �������, ����� �������� ���������� ������ ������
    for(int t = 0; t < 36; t++){
      for(int i = 0; i < 36; i++){
        if(m_Card[t]->m_State == TABLECARD){// ���� ����� ����� ������� �� �����
          if(m_Card[i]->m_State == GOINGPLAYER){// ���� ������������ ����� ����������� �������� ������
            if(m_Card[t]->m_Mast == m_Card[i]->m_Mast)// ���� ����� ���� ���� ���������
              if(m_Card[t]->m_Value == (m_Card[i]->m_Value+1) ||  // �������� ����� ������������ �� 1 ������ ��� ������
                 m_Card[t]->m_Value == (m_Card[i]->m_Value-1)   ){// ��� ����� �� �����

                m_Card[i]->CardOpen(true);//��������� �����
                //��������������� ���
                DoGoCard(m_Card[i]);
                is_go = true;// ��� ������
                BREAK = true;
                break;
              }
          }// m_Card[i]->m_State == GOINGPLAYER
        }// if(m_Card[t]->m_State == TABLECARD){
      }
      if(BREAK == true)break;
      
    }// for(int t = 0; t < 36; t++){

    // ���� ��� �� ������ �����-������ �� ��������
    // ���� ������� ���� ���� �����
    if(is_go == false){

      for(int i = 0; i < 36; i++){
        if(m_Card[i]->m_State == GOINGPLAYER &&
           m_Card[i]->m_Value == e_9){

            m_Card[i]->CardOpen(true);//��������� �����
            //��������������� ���
            DoGoCard(m_Card[i]);
            break;
          }
      }
    }
  }//else{ if(m_IsNewParty == true && m_IsFirstMove == true){


}


// ������� ���� ���� ������������
bool TMainForm::GoUser(TFCard* card)
{
  //��� ������� ������������� ��������� ��� ������������
  //�.�. ���� �� ��� ���, ������ ������
  if(GOINGPLAYER != USERPLAYER) return false;

  
  //����� ������� ��� ������� ������������
  LTablo->Visible = false;

  //���� ������ ��� � ����
  if(m_IsFirstMove == true){

    // � ������ ����� ������
    if(m_IsNewParty == true){
      // ������ ����� ������ �������� �����
      if(card->m_Mast != e_bubi || card->m_Value != e_9){
        LTablo->Visible = true;
        LTablo->Caption = "������ ��� �������� �����!";


        TimerMain->Enabled = true;// ��� �������� � ��������� ������� ��� �������
        return false;
      }
      else{
        //��������������� ���
        DoGoCard(card);
      }
    }
    else{//���� ��� �� ������ ����� ������
      // ������ ����� ����� �������� � ������ ��������
      if(card->m_Value != e_9){
        LTablo->Visible = true;
        LTablo->Caption = "������ ��� ������ ��������!";
        TimerMain->Enabled = true;// ��� �������� � ��������� ������� ��� �������
        return false;
      }
      else{
        //��������������� ���
        DoGoCard(card);
      }
    }
    
  }//if(m_IsFirstMove == true){
  else{// ���� �� ������ ��� � ����
    //������ ����� ����� ������ �������� ������� - ����� ������ ���� ��������
    // ��� ��������� �� �����
    // � ������� ������ �� ����� � ���� �� �������� �� ������� ������ ��� ������
    // ����� �� �����
    bool is_go = false;

    if(card->m_Value == e_9){
      //��������������� ���
      DoGoCard(card);
      is_go = true;
    }

    for(int t = 0; t < 36; t++){
      if(m_Card[t]->m_State == TABLECARD   &&
         m_Card[t]->m_Mast == card->m_Mast &&
         (m_Card[t]->m_Value == (card->m_Value+1) ||
         m_Card[t]->m_Value == (card->m_Value-1))  ){

         //��������������� ���
         DoGoCard(card);
         is_go = true;
         break;
      }
    }
    if(is_go == false){
      LTablo->Visible = true;
      LTablo->Caption = "���� ������ ������ ������!";
      TimerMain->Enabled = true;
    }

  }

  
  return true;
}


// ������� ������������ ��� ����� � ������ ������
// ����� ������ TimerMainTimer(...)
void TMainForm::WhoGoes()
{

  // ���� ���������� ����� ������
  // ��������� ��� ������ ������
  if(m_IsNewParty == true && m_IsFirstMove == true){
    // ���� � ���� 9 �����
    for(int i = 0; i < 36; i++){
      if(m_Card[i]->m_Value == e_9 && m_Card[i]->m_Mast == e_bubi){
        //���� 9 ����� �����, ���������� � ���� ������ ��� ���������
        // ��� � ����� ������
        GOINGPLAYER = m_Card[i]->m_State;
        //������ ���������, ���� �������������
        break;
      }
    }
  }// if(m_IsNewGame == true){

  //����� ����� ������ ��������� �� ������� �� ���� �����
  if(IsProcat() == true){

    // ��������������� ������
    DoProcat();

    GOINGPLAYER++;
    if(GOINGPLAYER == 4) GOINGPLAYER = 1;
    TimerMain->Enabled = true;
  }
  else{

    // ������ ��� ����� ��������� ������� ������
    if(GOINGPLAYER != USERPLAYER){//��������� ������ ��� ������������ �������
    // ������ ��� ������������ �������������� �������� ������
      GoComp();
      TimerMain->Enabled = true;
      //LTablo->Caption = GOINGPLAYER;
    }
    else{
      LTablo->Visible = true;
      LTablo->Caption = "��� ���!";
    }
  }
}

// ��������������� ������
void TMainForm::DoProcat()
{
  // ������ ������� �������
  Beep(1000, 50);
  Beep(500, 50);
  Beep(2000, 50);

  LTablo->Visible = true;
  if(GOINGPLAYER == 1)
      LTablo->Caption = "�� �����������!";
  if(GOINGPLAYER == 2)
      LTablo->Caption = "������ ����������!";
  if(GOINGPLAYER == 3)
      LTablo->Caption = "����� ����������!";


}

// ������� ������������ ������������� �� ������� �����
bool TMainForm::IsProcat()
{
  if(m_IsFirstMove == true && m_IsNewParty == true) return false;
  bool is_procat = true;
  bool BREAK = false;
  // ���� ����� ������� ����� ������  �����
  for(int t = 0; t < 36; t++){
    for(int i = 0; i < 36; i++){
      if(m_Card[t]->m_State == TABLECARD){// ���� ����� ����� ������� �� �����
        if(m_Card[i]->m_State == GOINGPLAYER){// ���� ������������ ����� ����������� �������� ������
          if(m_Card[t]->m_Mast == m_Card[i]->m_Mast)// ���� ����� ���� ���� ���������
            if(m_Card[t]->m_Value == (m_Card[i]->m_Value+1) ||
               m_Card[t]->m_Value == (m_Card[i]->m_Value-1)   ){

                is_procat = false;
                BREAK = true;
                break;
            }
        }// m_Card[i]->m_State == GOINGPLAYER
      }// if(m_Card[t]->m_State == TABLECARD){
    }
    if(BREAK == true)
      break;
  }// for(int t = 0; t < 36; t++){

  // ��� ���� ���� �������
  for(int i = 0; i < 36; i++){
    if(m_Card[i]->m_State == GOINGPLAYER &&// ���� ������������ ����� ����������� �������� ������
       m_Card[i]->m_Value == e_9){//  � ��� �������
       is_procat = false;
       break;
    }
  }

  // ���� ����� ��� �� ��������
  // �� ������ ����� � ����
  if(is_procat == true){
    MoneyToBank(GOINGPLAYER);
  }
  
  return is_procat;
}


// ������� �������� �� ��������� ������� ����
bool TMainForm::CheckEndGame()
{
  bool is_endgame = true;
  for(int i = 0; i < 36; i++){
    if(m_Card[i]->m_State == GOINGPLAYER){// ���� ����� ����������� ��������� ������
      // ���� ���� ���� ����� ���� � ��������� ������
      // � ����� ���� �������� ��� ����
      is_endgame = false;
      break;
    }
  }
  // ���� ������ ���� � ����� ������ ���������,
  // ��� ����������� ������ ���������� ������������ ������� ����
  // "��������� ����"
  if(is_endgame == true){

    // ���� �����������, ��� ���������� ��� �����
    // ��� ���� ����� ����� ��������� ����
    // ����� �������� ������������ ��� ������� �� ��������� ��
    // � ����������� ����, � ������ ������ ��� ���������� ����� ���������
    // �������� IsEndGame() ��������� ��� ��������� ��� public
    // � �������� �� ������ �������
    m_bEndGame = true;
    m_IsNewParty = false;
    NNextGame->Enabled      = true;
    NPopupNextgame->Enabled = true;
    TimerMain->Enabled = false;

    MoneyToPlayer(GOINGPLAYER);// ������ �� ����� ����������
  }

  return is_endgame;
}


// ������� ��������������� �������� ���� ������� �� ����
bool TMainForm::DoGoCard(TFCard* card)
{
  card->m_State = TABLECARD;
  // �� ����� � ����������� ����� ������ �� ����� �� ������� �����
  card->Left = CoordinatesTableCard[card->m_Mast-1][card->m_Value-1].x;
  card->Top  = CoordinatesTableCard[card->m_Mast-1][card->m_Value-1].y;

  // ����� ���� ����� ������ ��� ��� ����������
  m_IsFirstMove = false;
  if(CheckEndGame() == false){// ���� ���� �� ��������
    GOINGPLAYER++;
    if(GOINGPLAYER == 4) GOINGPLAYER = 1;
    // ���� ��� � ������� ��������� ������ ����
    TimerMain->Enabled = true;
  }
  else{ // ���� ���� ��������
    //ShowMessage("���� ���������!");


    // ���� ������ �� ��������
    if(CheckEndParty() == false){
      LTablo->Visible = true;
      LTablo->Caption = "���� ���������!";
      MessageBox(Handle, "���� ���������!", "��������", MB_OK);
    }
    else{// �����
      NNextGame->Enabled      = false;
      NPopupNextgame->Enabled = false;
      m_bEndParty = false;
     
    }
  }

  return true;
}


// ������� ������������� ������
void TMainForm::ShufflePack()
{
  int r;
  TFCard* temp;
  for(int i = 0; i < 36; i++){
    temp = m_Card[i];// ����� ������� ��� �� ����������� �� ������ ����������
    r = random(36);// ������� ������ ��� ����� ��������� ����� �� 0 �� 35
    m_Card[i] = m_Card[r];// �� ����� ���������� ���������� �������� ��������� �����
    m_Card[r] = temp;// � �� ����� ��������� ����� ���������� ����������
    // ����� �������� ���������� ��� ������
  }
  
  // ����� ������� ����� ����������� ���� ��� ������
  for(int j = 0; j < 36; j++){
    m_Card[j]->BringToFront();
  }
}


// ������ �� ����� � �����
// ������� ���� � ��� ������� �������
void TMainForm::MoneyToBank(int player)
{
  switch(player){
    case USERPLAYER:
      if(m_UserMoney != 0)
      {
        m_UserMoney -= 10;
        m_BankMoney += 10;
      }
      break;
    case LEFTPLAYER:
      if(m_LeftMoney != 0)
      {
        m_LeftMoney -= 10;
        m_BankMoney += 10;
      }
      break;
    case RIGHTPLAYER:
      if(m_RightMoney != 0)
      {
        m_RightMoney -= 10;
        m_BankMoney += 10;
      }
      break;
  }
  
  InitMoney(false);
}


// ������� ������ �������� - ����������� ���� ��� ���
bool TMainForm::IsEndGame()
{
  return m_bEndGame;
}


// ������ �� ����� ����������
// ����� ��������� ����
void TMainForm::MoneyToPlayer(int winner)
{
  switch(winner){
    case USERPLAYER:
      m_UserMoney += m_BankMoney;
      m_BankMoney  = 0;
      break;
    case LEFTPLAYER:
      m_LeftMoney += m_BankMoney;
      m_BankMoney  = 0;
      break;
    case RIGHTPLAYER:
      m_RightMoney += m_BankMoney;
      m_BankMoney   = 0;
      break;
  }
  InitMoney(false);
}


// ��������� ����������� ����� ������������
void TMainForm::UserCardsPos()
{
   // ������� ������ ������� ������ � ������������
   int countmast = 0;
   for(int m = 1; m <= 4; m++)
    for(int i = 0; i < 36; i++){
      if (m_Card[i]->m_State == USERPLAYER &&
          m_Card[i]->m_Mast  == m){
          countmast++;
          break;
      }
   }

   // ���������� ����� ������� ������ ������
   int distance = 30;

   // ���������� ���������� �� �������� ������� ��������� ��� ����� ������������
   // ����� ���������� �� ���������� ������ ����� ������������
   // ���� ����� �� ��������
   int add = (distance/2) * (countmast-1);
   
   // ������� ���������� ���� ������������ �����
   for(int i = 0; i < 12; i++)
    UserCardLeft[i] -= add;


  // ����� ����������� ����� ������������ �� ������(� �� ���������) � � ������������
  int countcarduser = 0;
  bool is = false;
  for(int mast = 1; mast <= 4; mast++){// ����� ������������ �����
    for(int value = 1; value <= 9; value++){// ����� � ���� ����� ���� ����� ������� � ����� ���������
      for(int i = 0; i < 36; i++){// � �� ���� ���� �������� ������ ����� ������������
        if(m_Card[i]->m_State == USERPLAYER &&
           m_Card[i]->m_Mast  == mast &&
           m_Card[i]->m_Value == value){
           m_Card[i]->Left = UserCardLeft[countcarduser++];
           m_Card[i]->BringToFront();
           
           is = true;// ���� ����� ����� ���� �������� ���
        }
      }// for(int i = 0; i < 36; i++){
    }// for(int value = 1; value <= 9; value++){
    
    if(is == true){
      //UserCardLeft[countcarduser] += distance;
      for(int d = 0; d < 12; d++)
          UserCardLeft[d] += distance;// ��� ������ ����������� ������ �����, �������� ���������� ����� ������������

      is = false;// ���������� ������� ������������� �����
    }
    
  }// for(int m = 1; m <= 4; m++){

}




// �������� �� ��������� ������
bool TMainForm::CheckEndParty()
{

  //���� � ����-���� �� ������� ����������� ������ ������ ������ ��������
  if(m_UserMoney == 0 || m_LeftMoney == 0 || m_RightMoney == 0){
    m_bEndParty = true;// ���������, ��� ������ ��������.
  }
  else{
    return false;
  }

  // ����� ���� ���������� ����������, �.�. � ���� ������ ���� �����
  int money = 300;// ���������� ��������� ���������� ���������� �����

  AnsiString S, sUser, sLeft, sRight;

  // ����������� ������� �����
  int temp;
  bool is = false;
  for(int i = money; i >= 0; i--){
    if(m_UserMoney == i){
      is = true;
      S = "����� "      + LNameUser->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_UserMoney)  + "���."  + "\n";
    }
    if(m_LeftMoney == i){
      is = true;
      sLeft = "����� "  + LNameLeft->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_LeftMoney)  + "���."  + "\n";
      S += sLeft;
    }
    if(m_RightMoney == i){
      is = true;
      sRight = "����� " + LNameRight->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_RightMoney) + "���."  + "\n";
      S += sRight;
    }

    // ���� ���������� �� ������ ����� ���������, ������������� ����
    if(is == true){
      temp = i;
      break;
    }
  }

  // ����������� ������� �����
  is = false;
  for(int i = (temp-1); i >= 0; i--){
    if(m_UserMoney == i){
      is = true;
      sUser = "����� "  + LNameUser->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_UserMoney) + "���."  + "\n";
      S += sUser;
    }
    if(m_LeftMoney == i){
      is = true;
      sLeft = "����� "  + LNameLeft->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_LeftMoney) + "���."  + "\n";
      S += sLeft;
    }
    if(m_RightMoney == i){
      is = true;
      sRight = "����� " + LNameRight->Caption + " ����� ������ �����"+ "\t- " + IntToStr(m_RightMoney) + "���."  + "\n";
      S += sRight;
    }

    // ���� ���������� �� ������ ����� ���������, ������������� ����
    if(is == true){
      temp = i;
      break;
    }
  }

  // ����������� �������� �����
  is = false;
  for(int i = (temp-1); i >= 0; i--){
    if(m_UserMoney == i){
      is = true;
      sUser = "����� "  + LNameUser->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_UserMoney) + "���."  + "\n";
      S += sUser;
    }
    if(m_LeftMoney == i){
      is = true;
      sLeft = "����� "  + LNameLeft->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_LeftMoney) + "���."  + "\n";
      S += sLeft;
    }
    if(m_RightMoney == i){
      is = true;
      sRight = "����� " + LNameRight->Caption + " ����� ������ �����" + "\t- " + IntToStr(m_RightMoney) + "���."  + "\n";
      S += sRight;
    }

    // ���� ���������� �� ������ ����� ���������, ������������� ����
    if(is == true){
      break;
    }
  }

  MessageBox(Handle, "   ������ ��������!   ", "��������", MB_OK);

  // ��� ������ ���������� ������
  // ����������� ������ � �������
  //         ������� ����   ����� � ����      �����������         ��� ������
  MessageBox(   Handle,        S.c_str(),       "����������",       MB_OK);

  return m_bEndParty;

}


