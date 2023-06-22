<template>
  <div id="app">
    <header-bar></header-bar>
    <router-view></router-view>
  </div>
</template>

<script>
import HeaderBar from "@/components/HeaderBar.vue";
export default {
  components: {
    HeaderBar,
  },
  name: "App",
};
</script>