<template>
  <div class="fireworks" @click="createFirework($event)" ref="fireworksContainer">
    <div class="firework" v-for="(firework, index) in fireworks" :key="index" :style="{ top: firework.y + 'px', left: firework.x + 'px', background: firework.color, opacity: firework.opacity }"></div>
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

      for (let i = 0; i < 30; i++) {
        const firework = {
          x,
          y,
          color: `hsl(${Math.random() * 360}, 100%, 50%)`,
          speedX: (Math.random() - 0.5) * 8,
          speedY: (Math.random() - 0.5) * 8,
          opacity: 1,
          fade: 0.03
        };
        this.fireworks.push(firework);
        setTimeout(() => {
          this.fireworks.splice(this.fireworks.indexOf(firework), 1);
        }, 1200);
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

<style scoped>
.fireworks {
  position: relative;
  width: 100vw;
  height: 100vh;
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
