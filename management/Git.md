# Git 配置

## 安装Git
[官网下载地址](https://git-scm.com/downloads)

### Windows
安装包下载完，打开.exe，按照默认选项安装即可。在任意空白处单机右键，可以看到Git Bash Here就下载成功了！


### macOS

#### 用brew

First, install the Homebrew package manager if it is not already installed:

```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

Use Homebrew to install git:

```
brew install git
```

#### 其他

[https://git-scm.com/download/mac](https://git-scm.com/download/mac)


### On Ubuntu and Debian
```
sudo apt update && sudo apt install git
```

## 生成自己的SSH key pair
把本地的文件push到github或者pull代码到本地时，github需要知道你是不是有权限，所以我们需要两个key，一个private，一个public，private key保存在本地user个人目录下的`.ssh`文件夹中，public key复制给github，就可以顺利操作了

### 安装OpenSSH
#### Windows

Follow the instructions under “Enable OpenSSH Client in Windows 10” on [https://winaero.com/blog/enable-openssh-client-windows-10/](https://winaero.com/blog/enable-openssh-client-windows-10/) 或 [https://learn.microsoft.com/zh-cn/windows-server/administration/openssh/openssh_install_firstuse]()


#### macOS

系统自带

#### Ubuntu and Debian

```
sudo apt update && sudo apt install openssh-client
```

### 用OpenSSH生成私钥和公钥
#### Windows
[官网教程](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent#generating-a-new-ssh-key)

1. 打开Git Bash，从开始菜单或者在空白处右键

2. 在命令行粘贴此行，并且把双引号中的内容换成本github账号的邮箱
```
ssh-keygen -t ed25519 -C "your_email@example.com"
```
或者
```
ssh-keygen -t rsa -b 4096 -C "your_email@example.com"
```
3. 命令行询问在哪里放置生成的密钥和公钥时，按回车键就会使用默认地址和钥匙名称(/c/Users/you/.ssh/id_xxx)。为了方便记忆，可以自己改名字

4. 命令行询问secure passphrase，可以直接回车不填。如果填了，每次push等操作的时候会要求多输入一次本密码

官网教程的ssh-agent及之后的部分可以忽略

#### macOS
同上

#### Ubuntu and Debian
同上

## 把public key交给Github
利用ssh-keygen生成公钥和私钥之后，去存放本钥匙的文件夹中，可以看到多出来的`id_xxx`和`id_xxx.pub`。用纯文本格式打开`id_xxx.pub`，复制全部内容

* 登陆Github
* Go to [https://github.com/settings/keys](https://github.com/settings/keys) 点击绿色的`New SSH key`按钮。`Title`自选，`Key type`不用管，在`Key`栏目，粘贴刚才复制的内容“`ssh-xxxxxxxx...`“
* 点击`Add key` 完成！


## Have a try
* 在一个你想放本仓库`Letter`的文件夹中，空白处右键，点击`Git bash Here`

* 浏览器Go to [https://github.com/Axiwa/Letter]()，如果你已经登陆，应该可以进入仓库界面

* 点击界面右边的Code->SSH->copy

* 在刚刚打开的Git bash命令行中

```
git clone git@github.com:Axiwa/Letter.git
```

* 文件夹中会多出来一个叫`Letter`的文件夹，点进去是我们的所有素材和代码

* 在scratch文件夹里随便添加点什么！

* 在Letter文件夹中右键，`Git bash Here`

* 依次执行以下三行命令

```bash
git add *
# 添加所有改动了的文件
```

```bash
git commit -m "info" 
# commit你的改动，此处的info是此次改动的信息，可以写任何你认为需要写的，比如 "add a new file xxx in scratch"
```

```bash
git push 
# 把本地的改动push到github上去！如果有security phrase，输入之即可
```
* 命令行显示成功，去看看你的改动是否push到了github！

## 如何协作
每次需要添加素材时，在本文件夹`Git bash Here`，命令行输入
```bash
git pull --no-rebase
# 将所有更新pull下来
```
添加或者修改文件之后，

```bash
git status
# 此命令可以看到所有未提交的改动
```

在Letter文件夹下的命令行中，输入
```bash
git add *
# 添加修改
```
或
```bash 
git rm ???
# ??? 为你删掉的文件
```
或
```bash 
git rm -r ???
# ??? 为你删掉的文件夹
```
直到 `git status`告诉你所有的改动都已经添加

```bash
git commit -m "info" # info为你想记录的修改信息
git push
```