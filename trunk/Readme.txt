0.2新加功能：
启动主程序后，按d可以设置目标域，
按p进入渗透模式，按x可以启动xss被动检测，按e可以启动一个编码转换工具

默认主界面命令：
c=Clear;清掉内存中的http session
d=Domain config; 设置需要捕获的域名，为空时捕获全部http请求
l=List session; 列出内存中的所有session
g=Collect Garbage;内存整理
h=Hosts config; hosts设置
w=write SAZ;手动生成
s=Toggle Forgetful Streaming; 
t=Toggle Title Counter; 
Q=Quit

如果在命令行以
capture.exe sethost
的方式启动，会在启动阶段出现配置hosts的对话框。