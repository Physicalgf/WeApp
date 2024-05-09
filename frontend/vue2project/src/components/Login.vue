<template>
<el-container>
  <el-header><i class="el-icon-user"> &nbsp;登录</i></el-header>
  <el-main>
    <h3 style="display: inline-block;">账号 ：</h3> <el-input v-model="formData.username" placeholder="请输入账号"></el-input>
    <br>
    <h3 style="display: inline-block;">密码 ：</h3><el-input placeholder="请输入密码" v-model="formData.password" show-password></el-input>
  </el-main>
  <el-footer>
    <el-button type="primary" @click="SubmitLogin"  style="font-size: 13px;">登录</el-button>
  </el-footer>
</el-container>
</template>

<script>
import axios from 'axios';

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
      // 在这里添加实际的登录逻辑，如调用API
      const response = axios.post('/api/login', this.formData);
      if (response.data.success) {
          // 登录成功，可以在这里处理登录成功后的逻辑，如设置用户信息、跳转到主页面等
          this.$router.push('/dashboard');
        }
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
