   M S B A M L   `   `   `  ????D  @SpaceWars, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SpaceWars	SpaceWars  5   	   /clr-namespace:SpaceWars.ViewSpaceWars.View  5   	   9!clr-namespace:SpaceWars.ViewModelSpaceWars.ViewModel  5   	       SpaceWars.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SpaceWars     5   	   .viewsclr-namespace:SpaceWars.View     5   	   8
viewmodels!clr-namespace:SpaceWars.ViewModel     5   	   -   5          ? Title$  
Space wars??5
   	   $	??600??6   $	??900??6)    ? 
ResizeMode$ NoResize=?65   ??5      ?? 5   
    ?? ImageSource$# /assets/images/background.jpg??6    j? Stretch$
 Fill=?6A    ? 	Resources 5       ?? DataType+   %SpaceWars.ViewModel.MainMenuViewModel!   SpaceWars.View.MainMenuView ? DataContext+  %SpaceWars.ViewModel.ControlsViewModel!   SpaceWars.View.ControlsView*  $SpaceWars.ViewModel.CreditsViewModel    SpaceWars.View.CreditsView*  $SpaceWars.ViewModel.EndMenuViewModel    SpaceWars.View.EndMenuView(	  "SpaceWars.ViewModel.IntroViewModel
   SpaceWars.View.IntroView'  !SpaceWars.ViewModel.GameViewModel   SpaceWars.View.GameView??%?  (??       5   1   *, +)(?? U     5   1   *, +)(?? ?     5   0   *, +)(?? ?     5   0   *, +)(?? R    5   .   *,	 +)(?? ?    5!   -   *, +)?? 5   
   MainMenuTemplate??"  61   .R?5         6!   ?? 5      ?? 5   
   ControlsTemplate??"  61   .R?5         6!   ?? 5      ?? 5   
   CreditsTemplate??"  60   .R?5         6    ?? 5      ?? 5   
   EndMenuTemplate??"  60   .R?5         6    ?? 5      ?? 5   
   IntroTemplate??" 	 6.   .R?5      
   6   ?? 5       ?? 5!   
   GameTemplate??"  6-   .R?5"         6   ?? 5#      5$      .??5%      ? .+?5&   
   ?? $??2??6-   $??0,0,0,0q?6A   $
|?True??6R   ??6   ?? 6R   5'      5(      