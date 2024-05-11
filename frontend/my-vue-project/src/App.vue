<template>
  <div id="app" class="fireworks" @click="createFirework($event)" ref="fireworksContainer">
    <div class="firework" v-for="(firework, index) in fireworks" :key="index" :style="{ top: firework.y + 'px', left: firework.x + 'px', background: firework.color, opacity: firework.opacity }"></div>
    <router-view/>
  </div>

</template>

<script>
export default {
  data() {
    return {
      fireworks: []
    };
  },
  methods: {
    createFirework(event) {
      const x = event.clientX;
      const y = event.clientY;

      for (let i = 0; i < 10; i++) {
        const firework = {
          x,
          y,
          color: `hsl(${Math.random() * 360}, 100%, 50%)`,
          speedX: (Math.random() - 0.5) * 2,
          speedY: (Math.random() - 0.5) * 2,
          opacity: 1,
          fade: 0.03
        };
        this.fireworks.push(firework);
        setTimeout(() => {
          this.fireworks.splice(this.fireworks.indexOf(firework), 1);
        }, 7000);
      }

      this.animateFireworks();
    },
    animateFireworks() {
      const animate = () => {
        for (let i = 0; i < this.fireworks.length; i++) {
          const firework = this.fireworks[i];
          firework.x += firework.speedX;
          firework.y += firework.speedY;
          firework.opacity -= firework.fade;
        }
        
        if (this.fireworks.length > 0) {
          requestAnimationFrame(animate);
        }
      };

      requestAnimationFrame(animate);
    }
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
html, body {
  margin: 0;
  padding: 0;
  height: 100%; /* 设置body和html的高度为100% */
  width: 100%;
  overflow: hidden; /* 防止滚动条出现 */
  display: flex; /* 使用flex布局 */
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
.fireworks {
  position: relative;
  width: 100%;
  height: 100%;
  overflow: hidden;
}

.firework {
  position: absolute;
  width: 5px;
  height: 5px;
  border-radius: 50%;
  pointer-events: none;
}
</style>
