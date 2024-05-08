<template>
    <transition name="fade">
      <div v-if="visible" class="message-box">
        <p>{{ message }}</p>
      </div>
    </transition>
  </template>
  
  <script>
  export default {
    props: {
      visible: {
        type: Boolean,
        required: true,
      },
      message: {
        type: String,
        required: true,
      },
    },
  };
  </script>
  
  <style scoped>
  .message-box {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    padding: 1rem;
    border-radius: ½rem;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  
    /* Add your desired styling here */
  }
  
  .fade-enter-active,
  .fade-leave-active {
    transition: opacity 0.3s ease;
  }
  
  .fade-enter-from,
  .fade-leave-to {
    opacity: 0;
  }
  </style>