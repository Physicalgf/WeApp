<template>
    <el-container>
      <div class="home" style="display: flex;">
        <div class="home-list">
          <div class="menu">
            <el-col :span="12">
              <el-menu
                default-active="2"
                class="el-menu-vertical-demo"
              >
                <el-menu-item index="1" @click="handleItemClick('1')">
                  <i class="el-icon-user-solid"></i>
                  <span slot="title">人员管理</span>
                </el-menu-item>
  
                <el-menu-item index="2" @click="handleItemClick('2')">
                  <i class="el-icon-service"></i>
                  <span slot="title">音乐管理</span>
                </el-menu-item>

                <el-menu-item index="3" @click="handleItemClick('3')">
                  <i class="el-icon-notebook-2"></i>
                  <span slot="title">专辑管理</span>
                </el-menu-item>
              </el-menu>
            </el-col>
          </div>
        </div>
        <div class="content">
          <!-- 人员管理组件 -->
          <people-management v-if="activeItem === '1'" />
          <!-- 音乐管理组件 -->
          <music-management v-if="activeItem === '2'" />
          <!-- 专辑管理组件 -->
          <album-management v-if="activeItem === '3'" />
        </div>
      </div>
    </el-container>
  </template>


<script>
import PeopleManagement from '@/components/Manage/ManageUser.vue';
import MusicManagement from '@/components/Manage/ManageMusics.vue';
import AlbumManagement from '@/components/Manage/ManageAlbum.vue';

export default {
  components: {
    PeopleManagement,
    MusicManagement,
    AlbumManagement
  },
  data() {
    return {
      activeItem: '', // 初始时没有选中任何项
    };
  },
  methods: {
    handleItemClick(itemIndex) {
      this.activeItem = itemIndex;
    },
  },
};
</script>

<style>
body {
    background-color: #e5e9ec;
}

.home {
    background-color: #ffffff;
    width: 80%;
    height: 1100px;
    margin-top: 3%;
    margin-left: 10%;
    border-radius: 50px;
}

.home-list {
    width: 15%;
    height: 100%;
    display: inline-block;
}

.el-menu {
    width: 240px;
}

.span {
    font-size: 5px;
}

.menu {
    background-color: antiquewhite;
    margin-top: 150px;
}

.content {
    background-color: #f5f7f8;
    width: 79%;
    height: 97%;
    margin-top: 15px;
    border-radius: 30px;
}

</style>