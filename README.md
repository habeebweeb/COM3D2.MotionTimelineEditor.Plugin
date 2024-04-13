# COM3D2.MotionTimelineEditor.Plugin

v1.1.3.0


## 概要

モーションをタイムラインで編集するプラグインです。

MMDに近い操作でモーションを作成することを目的にしています。


## インストール方法

`Sybaris\UnityInjector`ディレクトリに`COM3D2.MotionTimelineEditor.Plugin.dll`を配置してください。

COM3D2 Ver.2.33.1で動作確認済み。


## 必須MOD

複数メイド撮影プラグインver23.1
https://ux.getuploader.com/cm3d2_j/download/181


## 推奨MOD

[1.2.0.0]スタジオモードモーション自動ループプラグイン
https://ux.getuploader.com/com3d2_mod_kyouyu/download/181

これがないとマイポーズ再生時にループされません。


## 使い方の手順

1. **GUIの表示**: スタジオモード中か、エディット画面で`Ctrl+M`を押してGUIを表示します。歯車メニューの「TL EDIT」アイコンからも表示できます。

2. **タイムラインの作成**: メイドを配置した後、新規作成ボタンをクリックしてタイムラインを作成します。0Fには現在のポーズがキーフレームとして自動的に追加されます。

3. **ポーズ編集モード**: ポーズ編集のチェックをいれて、ポーズ編集モードを有効化します。

4. **フレームの選択**: タイムラインの上部に表示されている任意のフレーム数をクリックして、編集したいフレームを選択します。

5. **ポーズの変更**: IKをドラッグしてポーズを変更します。
タイムライン左側のボーン名をクリックすると、そのボーンの角度調整UIが表示されます。(スタジオモードのみ)

1. **キーフレームの追加**: `Enter`キーを押して、変更したボーンのキーフレームを追加します。

2. **確認**: 再生ボタン(`Space`キー)を押して、モーションを確認します。その後は3-7を繰り返して、モーションを作成します。

3. **タイムラインの保存**: セーブボタンを押してタイムラインを保存します。保存したタイムラインは、ロードウィンドウからいつでもロード可能です。

4.  **アニメの出力**: アニメ出力ボタンを押して、作成したモーションをマイポーズに出力します。


詳細な操作方法については、Twitterに上げている動画などを参照してください。
https://twitter.com/kidonaru/status/1775174898111091134


## キーボード操作

| キー            | 機能                     |
|-----------------|--------------------------|
| **Ctrl+M**      | プラグイン表示切り替え   |
| **Enter**       | キーフレームの追加 (ポーズ編集中のみ) |
| **Backspace**   | キーフレーム削除         |
| **Space**       | 再生/停止                |
| **F1**          | ポーズ編集モードの切り替え |
| **Ctrl+C**      | コピー                   |
| **Ctrl+V**      | ペースト                 |
| **Ctrl+Shift+V**| 反転ペースト             |
| **A**           | 前のフレームへ           |
| **D**           | 次のフレームへ           |
| **Ctrl+A**      | 前のポイントへ           |
| **Ctrl+D**      | 次のポイントへ           |
| **Shift**       | 複数選択                 |

一度起動すると下記パスに設定ファイルが作成されるので、
キーを変更したい場合はこちらを編集してください。
`Sybaris\UnityInjector\Config\MotionTimelineEditor.xml`


## 機能

### メインウィンドウ

![メインウィンドウ](img/img_01.jpg)

#### メインメニュー
- **新規作成**: タイムラインを新規に作成
- **ロード**: タイムラインのロードウィンドウを表示
- **セーブ**: 現在のタイムラインを保存
- **アニメ出力**: モーションをマイポーズに出力
- **キーフレーム**: キーフレーム詳細ウィンドウを表示
- **IK固定**: IK固定の設定ウィンドウを表示
- **動画再生**: 動画再生ウィンドウを表示
- **設定**: タイムラインの設定ウィンドウを表示

#### タイムライン操作
- **アニメ名**: モーションの名前を設定
- **最終フレーム番号**: 最終フレーム番号を設定
- **フレーム操作**: 現在のフレーム位置を操作
- **再生/停止**: モーションの再生と停止を切り替え
- **再生速度**: モーションの再生速度を調整

#### キーフレーム操作
- **追加**: 変更されたボーンをキーフレームに追加 (ポーズ編集中のみ)
- **全追加**: 全ボーンをキーフレームに追加 (ポーズ編集中のみ)
- **削除**: 選択中のキーフレームを削除
- **コピー**: 選択中のキーフレームをコピー
- **ペースト**: コピーしたキーフレームを現在のフレームに貼り付け
- **反転P**: 左右反転させて貼り付け

#### 選択操作
- **範囲選択**: 指定したフレーム範囲を選択
- **全選択**: すべてのキーフレームを選択
- **選択解除**: 選択中のキーフレームを解除
- **縦選択**: 選択しているフレーム内の全てのキーフレームを選択

#### モード切り替え
- **簡易編集**: フレーム内のキーフレームを一括で編集できるモードに切り替え
- **ポーズ編集**: ポーズ編集モードに切り替え
- **中心点IK表示**: 中心点IKの表示/非表示を切り替え
- **関節IK表示**: 各関節IKの表示/非表示を切り替え


### タイムライン操作

#### キーフレームの色
- **白**: 登録されているキーフレーム
- **赤**: 選択中のキーフレーム
- **グレー**: 一部のボーンが登録されているキーフレーム

#### 基本操作
- **フレーム番号**: クリックでそのフレームに移動
- **ボーン名**: 左のボーン名を選択で、そのボーンの角度調整UI表示 (ポーズ編集中のみ)
  - ボーンカテゴリのー＋をクリックで、そのカテゴリのボーンの表示/非表示を切り替え
- **ドラッグ**: キーフレームをドラッグして移動
- **エリア選択**: キーフレーム以外の場所をドラッグで、短形内のキーフレームを選択
- **複数選択**: `Shift`キーを押しながらキーフレームをクリックで複数選択


### キーフレーム詳細ウィンドウ

![キーフレーム詳細](img/img_04.jpg)

- **X, Y, Z**: 選択したキーフレームの位置を調整
  - テキストボックスに数値を入力すると、選択したキーフレームの位置がその値になります
  - 複数の値が存在する場合は、空欄になります
  - `<<` `<` `>` `>>`ボタンで、0.01、0.1ずつ相対的に移動します
  - 移動は中心ボーンのみ変更できます
- **RX, RY, RZ**: 選択したキーフレームの角度を調整
  - `<<` `<` `>` `>>`ボタンで、1、10ずつ相対的に変更します
- **初期化**: 選択したキーフレームの位置と角度を初期化
- **補間曲線**: 選択したキーフレームの補間曲線を表示します
- **表示種類**: 右の選択ボタンで、表示する補間曲線の種類を切り替えます
- **OutTangent**: 前のフレームのOutTangentを調整
- **InTangent**: 選択中のフレームのInTangentを調整
- **自動補間**: 自動補間を有効にする
- **プリセット反映**: プリセットを反映します
  - EaseInOut、EaseIn、EaseOut、Linearが選択可能


### IK固定ウィンドウ

![IK固定ウィンドウ](img/img_02.jpg)

各IKをワールド座標に固定化することができます。
ポーズ編集中のみ有効です。


### 動画再生ウィンドウ

![動画再生ウィンドウ](img/img_05.jpg)

- **有効**: 動画再生機能を有効にします
- **GUI表示**: GUIと3Dビュー表示を切り替えます
- **動画パス**: 動画再生用の動画パス
- **選択**: 動画を選択します
  - 動画は非圧縮aviを推奨します
- **再読み込み**: 動画を再読み込みします
- **開始位置**: 動画の開始位置を設定
- **X, Y, Z**: 動画の位置を調整
- **RX, RY, RZ**: 動画の角度を調整
- **表示サイズ**: 動画の表示サイズを設定
- **音量**: 動画の音量を設定


### 設定ウィンドウ

![設定ウィンドウ](img/img_03.jpg)

- **個別設定**: 各タイムライン個別の設定を行います
- **フレームレート**: タイムラインのフレームレートを設定
- **胸の物理無効**: 胸の物理演算を無効にする
- **ループアニメーション**: ループ再生用のアニメーションにします
  - 出力時、0Fのキーフレームをコピーして、最終フレームに貼り付けます
- **Tangent範囲**: 補間曲線の範囲を設定
- **初期化**: タイムライン個別設定を初期化

- **共通設定**: 全タイムライン共通の設定を行います
- **初期補間曲線**: 新規で追加するキーフレームの補間曲線を設定
- **Trans詳細表示数**: キーフレーム詳細ウィンドウで表示するTransformの最大数
  - キーフレーム詳細が重い場合は下げると改善することがあります
- **Tangent表示数**: キーフレーム詳細ウィンドウで表示する補間曲線の最大数
- **自動スクロール**: タイムラインの自動スクロールを有効にする
- **背景透過度**: タイムラインの背景の透過度を設定
- **初期化**: タイムライン共通設定を初期化


## タイムラインの保存形式

`COM3D2\PhotoModeData\_Timeline`ディレクトリに`アニメ名.xml`と、サムネ画像が`アニメ名.png`で保存されます。
タイムラインを共有したい場合は、このファイルを共有してください。

中身のサンプル:

```
<TimelineData>
  <Frame>
    <FrameNo>0</FrameNo>
    <Bone>
      <Transform>
        <Name>Bip01</Name>
        <Value>0</Value>
        ...
      </Transform>
    </Bone>
    ...
  </Frame>
  <AnmName>アニメ名</AnmName>
  <IsHold>false</IsHold>
  ...
  <IsLoopAnm>true</IsLoopAnm>
  <MaxFrameNo>30</MaxFrameNo>
  <FrameRate>30</FrameRate>
  <UseMuneKeyL>false</UseMuneKeyL>
  <UseMuneKeyR>false</UseMuneKeyR>
```


## 履歴

2024/04/14 v1.2.0.0
- 複数メイドプラグイン対応
  - 朝／夜メニューとエディット画面でも使用できます
  - ※複数メイドプラグインが必須となりました
- 動画再生機能の追加
  - タイムラインに合わせて動画を再生できるようになりました
- IK固定化機能の強化
  - 移動だけでなく回転などでも固定化が適用されます
- キーボード操作の追加
  - キーフレームのコピー、ペースト、反転ペーストなどが追加されました
  - 設定がリセットされるので、再設定が必要です

2024/04/07 v1.1.0.0
- キーフレームの一括値変更機能を追加
  - メインメニューの「キーフレーム」から使用できます
- 補間曲線の設定機能を追加
  - デフォルトが自動補間ありになっているため、前のバージョンと動きが変わります
  - 以前の挙動と合わせたい場合は、全フレームを選択してキーフレーム詳細を開き、プリセットで一番左のEaseInOutを選択してください

2024/04/03 v1.0.0.1
- しばりす2環境で動作するように修正

2024/04/02 v1.0.0.0
- 公開版リリース


## 規約


### 作成したデータについて

このプラグインで作成したタイムライン、モーションデータは自由に配布して問題ありません。

ただし、公式モーションをそのままコピーして配布したり、別ゲームでの利用などは禁止です。


### MOD規約

※MODはKISSサポート対象外です。
※MODを利用するに当たり、問題が発生してもKISSは一切の責任を負いかねます。
※「カスタムメイド3D2」か「カスタムオーダーメイド3D2」か「CR EditSystem」を購入されている方のみが利用できます。
※「カスタムメイド3D2」か「カスタムオーダーメイド3D2」か「CR EditSystem」上で表示する目的以外の利用は禁止します。
※これらの事項は http://kisskiss.tv/kiss/diary.php?no=558 を優先します。


個人の利用範囲でのプラグインの改造は自由ですが、.anmや.vmdを直接読み込む機能の追加は禁止です。

他の機能追加などをしたい場合は、リポジトリを公開しているのでこちらにPRをお願いします。
https://github.com/kidonaru/COM3D2.MotionTimelineEditor.Plugin

質問、要望などは@kidonaruまで (可能な範囲で対応します)
https://twitter.com/kidonaru


### プラグイン開発者向け

このプラグインの開発に手伝っていただける場合、下記手順でプルリクエストを送信してください。

1. このリポジトリをフォークします

2. フォークしたリポジトリを、ローカルの`COM3D2\Sybaris`以下にクローン
```bash
cd [COM3D2のインストールディレクトリ]\Sybaris
git clone https://github.com/[自分のユーザー名]/COM3D2.MotionTimelineEditor.Plugin.git
```

3. クローンしたディレクトリをVS Codeなどで開く

4. コード修正後、デバッグ用ビルドスクリプトを実行し動作確認
(自動でUnityInjector内にコピーされます)
```bash
.\debug.bat
```

5. 差分をリモートにプッシュして、フォーク元に対してプルリクエストを送信
