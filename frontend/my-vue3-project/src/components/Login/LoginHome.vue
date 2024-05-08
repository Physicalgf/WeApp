<style scoped>
.login-footer{
  margin-top: 20px;
  margin-left: 25px;
}
</style>


<template>
    <el-card shadow="always">


      <el-form ref="loginForm" :model="form" label-position="left" label-width="auto">
        <el-form-item label="用户名">
          <el-input v-model="form.username" placeholder="请输入用户名"></el-input>
        </el-form-item>
        <el-form-item label="密码">
          <el-input v-model="form.password" type="password" placeholder="请输入密码"></el-input>
        </el-form-item>
      </el-form>


      <el-row style="margin-top: 1rem;">
        <el-col :span="24" align="center">
          <el-button type="primary" @click="handleLogin">登录</el-button>
        </el-col>
      </el-row>


      <div class="login-footer">
       <el-button style="margin-right: 1rem;" @click="goToRegisPage">注册</el-button>
        |
      <el-button @click="goToForgotPassword">忘记密码？</el-button>
      </div>


      <el-dialog v-model:visible.sync="showErrorMessage" width="30%" center>
  <p>{{ errorMessage }}</p>
  <template #footer>
    <span class="dialog-footer">
      <el-button @click="showErrorMessage = false">确定</el-button>
    </span>
  </template>
</el-dialog>
    </el-card>
  </template>
  
  <script>
  import { ref, reactive } from 'vue';
  import axios from 'axios';
  import { inject } from 'vue'; 

  export default {
    methods: {
    goToForgotPassword() {
      this.$router.push('/forgetpage');
    },
    goToRegisPage() {
      this.$router.push('/regispage');
    },
  },
    setup() {
      const form = reactive({
        username: '',
        password: '',
      });
  
      const showErrorMessage = ref(false);
      const errorMessage = ref('');
      const apiUrl = inject('apiBaseUrl');
      
  
      async function handleLogin() {
        try {
          const response = await axios.post(`${apiUrl}/Login/GetData`, {
            
              UserName: form.username,
              PassWord: form.password,
            
          });
  
          if (response.data.code == '0') {
            // 登录成功，跳转到Home.vue页面
            // 假设您使用vue-router进行路由管理
            // 这里使用编程式导航进行跳转
            this.$router.push('/home');
          
          } else {
            showErrorMessage.value = true;
            errorMessage.value = `登录失败，服务器返回状态码: ${response.status}`;
          }
        } catch (error) {
          showErrorMessage.value = true;
          errorMessage.value = '登录失败，网络错误';
        }
      }
  
      return {
        form,
        handleLogin,
        showErrorMessage,
        errorMessage,
      };
    },
  };
  </script>