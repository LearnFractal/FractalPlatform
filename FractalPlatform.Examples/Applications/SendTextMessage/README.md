# Send Text Message

## Sources

- [Application](https://github.com/LearnFractal/FractalPlatform/tree/main/FractalPlatform.Examples/Applications/SendTextMessage/SendTextMessageApplication.cs)
- [Database](https://github.com/LearnFractal/FractalPlatform/tree/main/FractalPlatform.Examples/Databases/SendTextMessage)

## Functionality

Application sends message via Telegram (but it can be configured for other providers: Email, SMS, Skype, Viber).
Configuration:
1. Create a Telegram public channel
2. Create a Telegram BOT via @BotFather
3. Set the bot as administrator in your channel
4. Set Telegram.BotApiKey attribute in SendMessages dimension by bot api key that can be received from @BotFather.

Send text message:
1. Receiver - ChatId of created public channel in format: @MyChannel
2. Message - Text message to send

## How it implemented

**Video explanation**: No video

## Web Link

[SendTextMessage](https://fraplat.com/jupiter/?app=SendTextMessage)

