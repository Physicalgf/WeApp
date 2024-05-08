<template>
  <div class="register-page">
    <h1>注册新账号</h1>

    <el-form
      :model="form"
      :rules="rules"
      ref="registerForm"
      label-position="top"
      label-width="100px"
      @submit.prevent="handleRegister"
    >
      <el-form-item label="用户名" prop="username">
        <el-input v-model="form.username" placeholder="请输入用户名"></el-input>
      </el-form-item>

      <el-form-item label="密码" prop="password">
        <el-input
          type="password"
          v-model="form.password"
          placeholder="请输入密码"
        ></el-input>
      </el-form-item>

      <el-form-item label="确认密码" prop="confirmPassword">
        <el-input
          type="password"
          v-model="form.confirmPassword"
          placeholder="请再次输入密码"
        ></el-input>
      </el-form-item>

      <el-form-item>
        <el-button type="primary" native-type="submit">注册</el-button>
        <router-link to="/">已有账号？去登录</router-link>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { ref } from 'vue';
import axios from 'axios';
import { inject } from 'vue'; 

export default {
  setup() {
    const form = ref({
      UserName: '',
      PassWord: ''
      
    });
    const apiUrl = inject('apiBaseUrl');

    const rules = {
      UserName: [
        { required: true, message: '请输入用户名', trigger: 'blur' },
        { min: 3, max: 20, message: '用户名长度应在3到20个字符之间', trigger: 'blur' },
      ],
      PassWord: [
        { required: true, message: '请输入密码', trigger: 'blur' },
        { min: 6, max: 20, message: '密码长度应在6到20个字符之间', trigger: 'blur' },
      ],
      confirmPassword: [
        { required: true, message: '请再次输入密码', trigger: 'blur' },
        { validator: validateConfirmPassword, trigger: 'blur' },
      ],
    };

    function validateConfirmPassword(rule, value, callback) {
      if (value !== form.value.password) {
        callback(new Error('两次输入的密码不一致'));
      } else {
        callback();
      }
    }

    async function handleRegister() {
      try {
       const response = await axios.post(`${apiUrl}/Login/CreateAcc`, {
              UserName: form.value.username,
              PassWord: form.value.password
          });
        console.log(response.data.code);
        if (response.data.code == '0') {
            // 登录成功，跳转到Home.vue页面
            // 假设您使用vue-router进行路由管理
            // 这里使用编程式导航进行跳转
            //this.$router.push('/home');
          alert('登录成功');
          } else {
           
            alert('登录失败');
          }

      } catch (error) {
        console.error('注册失败:', error);
        // 失败时进行错误提示或处理
        alert('注册失败，请检查输入信息或稍后再试');
      }
    }

    return {
      form,
      rules,
      handleRegister,
    };
  },
};
</script>

<style scoped>
.register-page {
  width: 400px;
  margin: 50px auto;
}
</style>