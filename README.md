# Unity Namespace Batch Tool

Unity Editor 内で、特定フォルダーに入れた C# スクリプトに自動で namespace を付与するツールです。  
フォルダー構造に応じて namespace が生成されるため、既存のスクリプトを整理する際に便利です。

---

## 特徴

- 選択したフォルダー以下の全スクリプトに namespace を自動付与
- フォルダー階層をそのまま namespace として反映
- すでに namespace があるスクリプトはスキップ
- `using` 文は namespace 内に移動

---

## インストール

1. `Assets/Editor/` フォルダーを作成
2. `NamespaceBatchAdder.cs` を `Assets/Editor/` に配置
3. Unity が自動でコンパイルします

---

## 使い方

1. Project ビューで namespace を付与したいフォルダーを選択
2. メニューから **Tools → Add Namespace To Selected Folder** をクリック
3. 選択フォルダー以下の C# スクリプトに namespace が追加されます

---

## 例

フォルダー構造：

