# logger-mc-server

[![Build Status](https://travis-ci.org/bkaradzic/bgfx.svg?branch=master)](https://github.com/levshx/logger-mc-server/releases/tag/Ant)

Minecraft logger (Moderation tools)


![Main](https://sun9-51.userapi.com/GD3opq0rZy0cIYgKWrDuoYZFUNcsFXtlZK43Zg/E-cfp-GmimY.jpg)


### Message types:
* [G] Global chat
* [L] Local chat
* [C] Commands


### Triggers:
* Ads warp
* Ads commerce
* Words trigger
* Commands trigger
* Restart trigger
* Player join trigger

## More settings in settings.json
```json
  {
    "https": false,
    "chatUrl": "logs.s9.mcskill.ru/Technomagic2_public_logs/",
    "dropUrl": "logs.s9.mcskill.ru/Technomagic2_logger_public_logs/",
    "deathUrl": "logs.s9.mcskill.ru/Technomagic2_public_logs/",
    "adCommerce": true,
    "obscenity": true,
    "adWarp": true,
    "restartedTrigger": true,
    "adCommerceWords": [
      "продаю",
      "продам",
      "обменяю",
      "куплю"
    ],
    "adWarpWords": [
      "warp",
      "/warp",
      "варп"
    ],
    "commandsTrigger": [
      "/screenshot levshx",
      "/warp end",
      "/seen levshx",
      "/whois levshx",
      "/tp levshx",
      "/vanish"
    ],
    "obscenityWords": [
      "s*ka",
      "b**at",
      "н**уй",
      "б**дь",
      "кр*тин"
    ],
    "playerJoinTrigger": [
      "SkyDrive_",
      "Astrallis",
      "levshx",
      "moskovroma",
      "Flying_Joe",
      "n1ke374",
      "bng"
    ],
    "publicWordsTrigger": [
      "levshx",
      "лев",
      "левша"
    ],
    "localWordsTrigger": [
      "бесплатно",
      "верни",
      "дюп",
      "баг"
    ],
    "messageWordsTrigger": [
      "бесплатно",
      "пожалуйста",
      "дюп",
      "баг"
    ]
  }
```
