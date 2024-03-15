<template>  
    <navbar />
    <EventList :title="title" :isAthlete="isAthlete"/>
</template>
  
<script setup lang="ts">

import EventList from "../components/PeopleList.vue"
    import { defineProps } from "vue";
  import navbar from "../components/Navbar.vue"
  
  defineProps<{ title: String, isAthlete: Boolean }>();
  </script>