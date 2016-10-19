个人网站
========
----
####简单介绍
>使用的是基于.NetFrameWork4.5的ASP.NET WebAPI做的一个个人网站，当然还在进行中，准备分两个项目模块，一个是专门数据处理的，也就是api，这个模块主要使用的架构是基于三层架构的多层架构，使用抽象工厂（算是吧？）。另一个是专门展示交互的模块（html、css、js），如果可能的话还有其他平台的内容，当前只做web的



-----------
####功能模块

>登录注册找回密码一套

>文章撰写、修改、发布、评论

>图片发布、修改、评论

>个人信息设定、修改等

>用户个人模块

>后台管理及审核文章、图片

>后台用户管理管理模块

>后台功能规划页面


--------
####使用技术（支持）
| 名称     |  版本   |   说明  |
| :-------- | :--------| :------ |
| 正则表达式|*|检测匹配表单输入，过滤输入内容|
|Lambda表达式|基于 .NetFrameWork4.5|处理IEnumerable的数据，提取和修改内容|
|NLog|4.0|错误日志记录功能|
|Dapper|1.50.2|轻型的ORM框架，处理与数据库交互|
|AES|*|使用32位密钥加密短字符串，这里用于加密用户密码|
|GDI+绘图|*|生成自定义图片，这里用于处理生成验证码，[Demo地址](https://git.oschina.net/neclodiver/TestEleven/tree/master/VerificationCode?dir=1&filepath=VerificationCode&oid=eb0fc99d3b0d587336e71d1b5c64673115660ae6&sha=91b543fc8a6aa1939585942b8d74b40b55963a41)|
|Trie|*|用于敏感词判断,[几个demo](http://git.oschina.net/neclodiver/TestEleven)|
|BootStrap|3.3.0|用于搭建前端样式|


------
####使用工具

|名称|
|:----|
|Visual Studio 2015|


------


