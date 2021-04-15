EDWARD RAYMOND HE 518030990022

## 游戏名称：SEDAH

### 游戏类型：ROGUE LIKE - ACTION RPG

##### 玩法改动：目前为 纯闯关类 Rogue-Like 游戏，玩家通过一定地操作技巧消除每层的敌人后，将会获得胜利。该游戏目前暂未设计剧情。但是还原了部分随机化与操作挑战化的元素，我认为对我自身有比较大的启发。

##### 设计思路：对于这款游戏的雏形 Hades ，我最初认为并不算是2D，因此就作为一款3D游戏开发了

##### 	开发流程为：随机化生成地图、玩家与怪物的操作、动画衔接优化改善、游戏计分机制完善、道具完善等…

###### 1. 这款游戏最大的亮点之一是体现较多环境特性的随机化生成地图，在基于 Rogue-Like 地图（Chess-Maze算法）教程的基础上，通过使用 Scriptable Object 的特性，为每层地图生成新的地貌与环境，我自己在玩这款游戏的时候也时常被这一点所吸引。

###### 2. 第二大的亮点体现在游戏难度与一定难度要求的玩家操作。其中出乎意料的是 Animator 状态机的优化，因为游戏逻辑中 Character 之间的互动非常频繁，始终会发现状态机异常、不符合逻辑时产生的 Bug，因此在优化动画逻辑方面下了很多功夫，同时也让画面和操作手感上更加流畅舒适。

------



### 游戏已实现：

#### 环境特性：

随机化生成地图（融合教程、再进一步改进生成地貌对应特性：环境装饰等）

1. 地块随机化（每层）

2. 怪物种类、数量、分布随机化（每层）
3. 开始菜单中可设置地图的复杂性、地图层数

环境灯光随层数增高而逐渐变暗，玩家可通过周边随机生成的小灯塔进行挑战。

#### 游戏机制：

1. 每层关卡怪物难度调整（Animator的速度、属性随关卡层数递增）
2. 闯关奖励Buff 4类中随机生成3类

#### UI 界面：

1. 风格统一的开始、结束、暂停菜单、游戏面板提示与可交互UI

3. 摄像机画面周边滤镜随玩家血量百分比降低而改变透明度

4. 标题音频可视化（融合组件）

#### 视频音频：

1. 人物、怪物制作合理的状态机并衔接各动画

   在对应动画时刻调用特定函数（如武器挥出时抵达某帧调用Attack）

2. Character 交互播放音频

#### 简易AI：

1. 怪物将在特定范围内察觉到玩家，并追逐

------



### 参考素材：

1. ##### UI

   Basic_RPG_Icons, GUI_Parts, RPG icons, RPG_inventory_icons

2. ##### Model

   DungeonVoxels, Free!Low_Poly_Boxy, SkeletonOutlaw, Melee Warrior Animations, MediaVox, Monster_Orc, Suriyun, Voxy Legends, PowerUps

3. ##### Audio

   Blade Sounds, Bow and Hammer Sound Effects, Demo Ancient Weapons Pack, Game Audio Try Pack, SwordSoundPack, HADES: ORIGINAL SOUNDTRACK

4. ##### Audio Visualization

   Smitesoft

5. ##### Procedural Map

   https://www.youtube.com/watch?v=782NCelLfP8