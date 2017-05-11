terraform {
  backend "s3" {
    bucket  = "cv-overkill-tf-state"
    key     = "aws-infrastructure-experience"
    region  = "eu-west-1"
  }
}

variable "region" {
  default = "eu-west-1"
}

module "experience" {
  source = "github.com/mtranter/cv-overkill-terraform?ref=v1.0//modules/tf-cv-overkill-aurelia-module"
  website_files = ["app-bundle.js"]
  relative_source_path = "/../src/ui/dist/"
  region = "${var.region}"
  module_name = "experience"
}
