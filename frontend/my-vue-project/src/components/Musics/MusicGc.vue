<template>
    <div class="music-player">
        <div style="width: 300px;height: 300px;border-bottom: 10px solid #f5f5f5;">
            <audio ref="audioPlayer" style="width: 600px;margin-top: 60px;" controls :src="audioFileUrl" @timeupdate="handleTimeUpdate"></audio>
        </div>
     
      <div class="lyric-container" ref="lyricContainer">
        <div
          v-for="(line, index) in lyrics"
          :key="index"
          class="lyric-line"
          :class="{ 'current': index === currentLineIndex }"
          ref="lyricLine"
        >
          {{ line.content }}
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        audioFileUrl: 'http://121.40.185.118:8068/musics/成都.mp3',
        lyrics: [],
        currentLineIndex: 0,
        audioElement: null
      };
    },
    methods: {
      handleTimeUpdate() {
        const currentTime = Math.floor(this.$refs.audioPlayer.currentTime * 1000);
        for (let i = 0; i < this.lyrics.length; i++) {
          if (this.lyrics[i + 1] && currentTime > this.lyrics[i].time && currentTime < this.lyrics[i + 1].time) {
            this.currentLineIndex = i;
            this.scrollToCurrentLine();
            break;
          }
        }
      },
      scrollToCurrentLine() {
        const lyricLine = this.$refs.lyricLine[this.currentLineIndex];
        if (lyricLine) {
          const container = this.$refs.lyricContainer;
          container.scrollTop = lyricLine.offsetTop - container.offsetTop - container.clientHeight / 2;
        }
      },
      loadLyricsFromFile() {
        // 以下是一个简单的示例，您可能需要使用合适的方法从本地读取LRC文件内容
        const lyricssfa = `[00:00.000] 作词 : 赵雷\n[00:01.000] 作曲 : 赵雷\n[00:03.269]编曲：赵雷/喜子\n[00:05.409]\n[00:18.744]让我掉下眼泪的\n[00:22.138]不止昨夜的酒\n[00:26.187]让我依依不舍的\n[00:30.218]不止你的温柔\n[00:34.185]余路还要走多久\n[00:38.218]你攥着我的手\n[00:42.176]让我感到为难的\n[00:45.984]是挣扎的自由\n[00:52.186]分别总是在九月\n[00:55.935]回忆是思念的愁\n[00:59.878]深秋嫩绿的垂柳\n[01:03.802]亲吻着我额头\n[01:07.696]在那座阴雨的小城里\n[01:11.835]我从未忘记你\n[01:15.877]成都 带不走的 只有你\n[01:23.740]和我在成都的街头走一走\n[01:31.721]直到所有的灯都熄灭了也不停留\n[01:39.451]你会挽着我的衣袖\n[01:43.392]我会把手揣进裤兜\n[01:47.297]走到玉林路的尽头\n[01:51.364]坐在小酒馆的门口\n[02:31.189]分别总是在九月\n[02:34.726]回忆是思念的愁\n[02:38.523]深秋嫩绿的垂柳\n[02:42.552]亲吻着我额头\n[02:46.530]在那座阴雨的小城里\n[02:50.650]我从未忘记你\n[02:54.459]成都 带不走的 只有你\n[03:02.260]和我在成都的街头走一走\n[03:10.433]直到所有的灯都熄灭了也不停留\n[03:18.294]你会挽着我的衣袖\n[03:22.022]我会把手揣进裤兜\n[03:26.142]走到玉林路的尽头\n[03:30.164]坐在小酒馆的门口\n[03:38.409]和我在成都的街头走一走\n[03:46.065]直到所有的灯都熄灭了也不停留\n[03:54.019]和我在成都的街头走一走\n[04:01.809]直到所有的灯都熄灭了也不停留\n[04:09.659]你会挽着我的衣袖\n[04:13.564]我会把手揣进裤兜\n[04:17.530]走到玉林路的尽头\n[04:21.581]走过小酒馆的门口\n[04:36.177]和我在成都的街头走一走\n[04:43.454]直到所有的灯都熄灭了也不停留\n[04:53.227]\n[05:21.058]制作人：赵雷/喜子/姜北生\n[05:21.858]BASS：张岭\n[05:22.641]鼓：贝贝\n[05:23.388]钢琴：柳森\n[05:24.091]箱琴：赵雷/喜子\n[05:24.827]笛子：祝子\n[05:25.539]弦乐编写：柳森\n[05:26.258]弦乐：亚洲爱乐国际乐团\n[05:27.005]和声：朱奇迹/赵雷/旭东\n[05:27.698]童声：朵朵/天天`;
            this.lyrics = this.parseLrc(lyricssfa);
        
      },
      parseLrc(lrcData) {
        // 解析LRC文件内容为歌词数组
        // 以下是一个简单的示例，您可能需要根据LRC文件格式制定更复杂的解析逻辑
        const lines = lrcData.split('\n');
        const lyrics = lines.map(line => {
          const parts = line.split(']');
          const time = this.parseTime(parts[0]);
          const content = parts[1];
          return { time, content };
        });
        return lyrics;
      },
      parseTime(rawTime) {
        // 将LRC文件中的时间格式解析为毫秒
        // 以下是一个简单的示例，您可能需要根据LRC文件时间格式制定更复杂的解析逻辑
        const timeParts = rawTime.substr(1).split(':');
        const minutes = parseInt(timeParts[0]);
        const seconds = parseFloat(timeParts[1]);
        return (minutes * 60 + seconds) * 1000;
      }
    },
    mounted() {
      this.audioElement = this.$refs.audioPlayer;
      this.loadLyricsFromFile();
    }
  };
  </script>
  
  <style>
  .lyric-container {
    height: 300px; /* 设置歌词容器的固定高度 */
    overflow-y: auto; /* 为歌词启用垂直滚动 */
  }
  
  .lyric-line {
    padding: 5px 0;
  }
  
  .lyric-line.current {
    font-weight: bold; /* 应用当前歌词行的样式 */
    color: #ff0000; /* 应用当前歌词行的样式 */
  }
  </style>
