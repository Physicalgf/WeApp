<template>
<el-container>
  <el-header><i class="el-icon-user"> &nbsp;登录</i></el-header>
  <el-main>
    <h3 style="display: inline-block;">账号 ：</h3> <el-input v-model="formData.username" placeholder="请输入账号"></el-input>
    <br>
    <h3 style="display: inline-block;">密码 ：</h3><el-input placeholder="请输入密码" v-model="formData.password" show-password></el-input>
  </el-main>
  <el-footer>
    <router-link to="/register" style="font-size: 18px;margin-left: 25px;">注册</router-link>
    <el-button type="primary"  @click="SubmitLogin" style="font-size: 13px;margin-left: 40px;">登录</el-button>
    <router-link to="/forget" style="margin-left: 40px;font-size: 18px;">忘记密码</router-link>
  </el-footer>
</el-container>
</template>

<script>
import axios from 'axios'
import Router from '../router'

export default {
  data () {
    return {
      formData: {
        username: '',
        password: ''
      }
    }
  },
  methods: {
    SubmitLogin () {
      console.log('登录', this.formData)
      axios.post('https://localhost:44311/Login/GetData', this.formData).then(res => {
        if (res.data.code === 200) {
          Router.push('/home')
        }
      })
    }
  }
}
</script>

<style scoped>
.el-header {
 margin-top: 100px;
}
.el-icon-user {
  font-size: 40px; /* 根据需要调整图标大小 */
}
.el-input{
  width: 300px;
}
.a{
  font-size: 100px;
}
</style>
