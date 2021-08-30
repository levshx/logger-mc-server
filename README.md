# logger-mc-server

<!-- # Short Description -->

Minecraft server LOGGER
[![Github REPORT](https://img.shields.io/static/v1?label=GITHUB&message=REPORT%20BUGS&style=for-the-badge&logo=GitHub)](https://github.com/levshx/logger-mc-server/issues)
<!-- # Badges -->

[![Github issues](https://img.shields.io/github/issues/levshx/logger-mc-server)](https://github.com/levshx/logger-mc-server/issues)
[![Github forks](https://img.shields.io/github/forks/levshx/logger-mc-server)](https://github.com/levshx/logger-mc-server/network/members)
[![Github stars](https://img.shields.io/github/stars/levshx/logger-mc-server)](https://github.com/levshx/logger-mc-server/stargazers)
[![Github top language](https://img.shields.io/github/languages/top/levshx/logger-mc-server)](https://github.com/levshx/logger-mc-server/)
[![Github license](https://img.shields.io/github/license/levshx/logger-mc-server)](https://github.com/levshx/logger-mc-server/)

# Tags

`C#` `.NET` `Minecraft` `Server`

# Demo

![Demo](resources/file-0.jpeg)

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

### More settings in settings.json
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

# Installation

Download #Ant [Release](https://github.com/levshx/logger-mc-server/releases/tag/Ant)

# Deployment

Install in nuget:
* Newtonsoft.Json
* MetroFramework

# Contributors

- [levshx](https://github.com/levshx)

<!-- CREATED_BY_LEADYOU_README_GENERATOR -->
