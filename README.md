# チキ・チキ・チキンゲーム

### 開発目標

AndroidとPCでのクロスプレイを実装し、手軽に遊べるカジュアルゲームを作る！


### 工夫した点、努力した点

AndroidとPCでのPlayerの操作感をなるべく近づけ、デバイスによって有利不利がなるべく生じないようにしました。

Buttonの大きさ、位置を任意の位置に変更できるようにし、ユーザーが操作しやすい設定に変更できるようにしました。

FileManagerを作成し、設定データ(感度、音量、チュートリアルの有無、Buttonの大きさ位置)を保存することで、再び遊ぶ時に前回の設定で遊べるようにしました。


### 現状

現状ソロプレイのみとなっています。

対人戦は今後、Photonを用いてNetworkを実装予定です。

Player待機画面は対人戦用に作成したものです。Startボタンを押すことで遊べます。

Playerが1人称でありキャラを見ることができないので、3人称でのカメラを実装予定です。


### 使用アセット

[Font](https://atclip.jp/font-page/161)

[FadeManager](https://github.com/naichilab/Unity-FadeManager/blob/master/README.ja.md)

[キャラクター](https://assetstore.unity.com/packages/3d/characters/animals/5-animated-voxel-animals-145754)

[UI](https://assetstore.unity.com/packages/2d/gui/icons/simple-ui-icons-147101)

[マウスカーソル](https://assetstore.unity.com/packages/2d/gui/icons/pixel-cursors-109256)

[JoyStick](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631)

[SkyBox](https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400)


### 開発環境と言語

Unity 2019.4.1f1

C#
