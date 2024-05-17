<template>
  <div>
    <el-row type="flex" class="row-bg" justify="center">
      <el-col :span="8">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span>我的音乐</span>
          </div>
          <div class="text item">
            <h2>{{ currentSong.title }}</h2>
            <p>{{ currentSong.artist }}</p>
          </div>
          <audio controls :src="currentSong.audioUrl"></audio>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      currentSong: {
        title: '我记得',
        artist: '赵雷',
        audioUrl: 'http://121.40.185.118:8068/musics/mp31.mp3'
      }
    };
  }
};
</script>

<style>
.item {
  text-align: center;
  margin-bottom: 20px;
}

.box-card {
  width: 400px;
  margin-top: 20px;
}
</style>
