# LevelEditor-TowerDefense beta version
说明：
  该编辑器是基于Unity TileMap进行实现的，通过更改画笔配置和关卡信息(ScriptableObject)的怪物波次配置达到对关卡的编辑。
![图片](https://user-images.githubusercontent.com/64729329/196085934-56c4665f-353c-4795-9c93-e4078eb14d84.png)
![图片](https://user-images.githubusercontent.com/64729329/196085961-e810b693-fe03-4e1a-8e3f-f6f16bb94947.png)
![图片](https://user-images.githubusercontent.com/64729329/196085989-dd3b7205-9563-48c5-8a58-ae7cf8186cd6.png)
使用方法：
  至少创建两层TileMap分为基础的地面层和放置路径点和子路径点的路径层。
在地面层铺好tile后根据怪物行走的路径要求和防御塔的建筑位置通过GameObject Brush为分别放置RoadTile和normalTile。
在TileMapManager处出入levelIndex点击saveLevel对关卡地图文件进行创建。再次用画笔在路径层放置waypoint。将路径层节点设置到WaypointManagerWindow中。使用connect to PrevWaypoint
进行对上一个路径点的连接。对于分支路线分支通过Create Branch Receiver设置为接收者，分支通过create Branch设置。连接使用connect all receiver to branch。
  对于多点汇合的地方通过create merge point receiver创建接收者，而汇合的点通过create merge point创建。最终通过connect all receiver to mergePoint。子路径点则通过GameObject B-
rush放置subwaypoint并手动在inspector面板分配属性设置。通过connect subwaypoint to waypoints即可一键对子路径点合并到路径点中。最终在tilemapManager中通过Save Blocks与Save Wayp-
oints按钮对地图进行保存
