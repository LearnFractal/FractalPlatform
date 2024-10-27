# Dating Game

## Sources

- [Application](https://github.com/LearnFractal/FractalPlatform/tree/main/FractalPlatform.Examples/Applications/DatingGame/DatingGameApplication.cs)
- [Database](https://github.com/LearnFractal/FractalPlatform/tree/main/FractalPlatform.Examples/Databases/DatingGame)

## Functionality

Application moderates multiplayer dating game between boys and girls.
- On first stage three incognito boys and three incognito girls ask three questions to each other
- On second stage each boy and each girl should to answer on questions of opponents
- On third stage boys and girls review answers of their opponents
- On fourth stage each boy and girl can choose the best sympathy to continue dating
- On fifth stage the server try find matches between boys and girls 
   and if it is mutual sympathy then contacts (photo, messanger) will be opened for matched pair

Details of implementation
- Server can start infinity parallel games
- Game is started when we have 3 boys (at least 1) and 3 girls (at least 1)
- If an user don't want continue game, the game will be continued with rest of participants by timeout (3 minutes)

## How it implemented

**Video explanation**: No video

## Web Link

[DatingGame](https://fraplat.com/jupiter/?app=DatingGame)

## Open app in Fractal Studio

[Play with DatingGame in sandbox](https://fraplat.com/mars/FractalStudio/?tag=DatingGame+template)


